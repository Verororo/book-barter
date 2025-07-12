
using BookBarter.Application.Common.Interfaces.Repositories;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using BookBarter.Application.Common.Interfaces;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class DeleteOwnedBookCommand : IRequest
{
    public int BookId { get; set; }
}

public class DeleteOwnedBookCommandHandler : IRequestHandler<DeleteOwnedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly ICurrentUserProvider _currentUserProvider;
    public DeleteOwnedBookCommandHandler(IGenericRepository repository, ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task Handle(DeleteOwnedBookCommand request, CancellationToken cancellationToken)
    {
        var userId = (int)_currentUserProvider.UserId!;

        var ownedBooks = await _repository.GetByPredicateAsync<OwnedBook>
            (ob => ob.UserId == userId && ob.BookId == request.BookId, cancellationToken);
        if (!ownedBooks.Any()) { throw new EntityNotFoundException($"User {userId} doesn't own the book {request.BookId}."); }

        _repository.Delete(ownedBooks);

        await _repository.SaveAsync(cancellationToken);
    }
}
