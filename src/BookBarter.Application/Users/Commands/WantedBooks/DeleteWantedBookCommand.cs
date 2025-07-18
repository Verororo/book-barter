
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
        var userId = (int)_currentUserProvider.UserId!;  // FIX: ensure UserId is not null

        var wantedBooks = await _repository.GetByPredicateAsync<WantedBook>
            (ob => ob.UserId == userId && ob.BookId == request.BookId, cancellationToken);
        if (!wantedBooks.Any()) { throw new EntityNotFoundException($"User {userId} doesn't want the book {request.BookId}."); }  // FIX: use .Count

        _repository.Delete(wantedBooks);

        await _repository.SaveAsync(cancellationToken);
    }
}
