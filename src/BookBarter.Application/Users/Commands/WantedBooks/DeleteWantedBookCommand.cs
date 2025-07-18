
using BookBarter.Application.Common.Interfaces.Repositories;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using BookBarter.Application.Common.Interfaces;

namespace BookBarter.Application.Users.Commands.WantedBooks;

public class DeleteWantedBookCommand : IRequest
{
    public int BookId { get; set; }
}

public class DeleteWantedBookCommandHandler : IRequestHandler<DeleteWantedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly ICurrentUserProvider _currentUserProvider;
    public DeleteWantedBookCommandHandler(IGenericRepository repository, ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task Handle(DeleteWantedBookCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserProvider.UserId.HasValue)
        {
            throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
        }
        var userId = _currentUserProvider.UserId.Value;

        var wantedBooks = await _repository.GetByPredicateAsync<WantedBook>
            (ob => ob.UserId == userId && ob.BookId == request.BookId, cancellationToken);
        if (wantedBooks.Count == 0) { throw new EntityNotFoundException($"User {userId} doesn't want the book {request.BookId}."); }

        _repository.Delete(wantedBooks);

        await _repository.SaveAsync(cancellationToken);
    }
}
