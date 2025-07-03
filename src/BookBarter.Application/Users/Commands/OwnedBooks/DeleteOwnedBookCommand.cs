
using BookBarter.Application.Common.Interfaces.Repositories;
using MediatR;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class DeleteOwnedBookCommand : IRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}

public class DeleteOwnedBookCommandHandler : IRequestHandler<DeleteOwnedBookCommand>
{
    private readonly IGenericRepository _repository;
    public DeleteOwnedBookCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteOwnedBookCommand request, CancellationToken cancellationToken)
    {
        var ownedBooks = await _repository.GetByPredicateAsync<OwnedBook>
            (ob => ob.UserId == request.UserId && ob.BookId == request.BookId, cancellationToken);
        if (!ownedBooks.Any()) { throw new Exception($"User {request.UserId} doesn't own the book {request.BookId}."); }

        _repository.Delete(ownedBooks);

        await _repository.SaveAsync(cancellationToken);
    }
}
