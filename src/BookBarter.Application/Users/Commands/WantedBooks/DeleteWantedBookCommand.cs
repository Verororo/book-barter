
using BookBarter.Application.Common.Interfaces.Repositories;
using MediatR;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Commands.WantedBooks;

public class DeleteWantedBookCommand : IRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}

public class DeleteWantedBookCommandHandler : IRequestHandler<DeleteWantedBookCommand>
{
    private readonly IGenericRepository _repository;
    public DeleteWantedBookCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteWantedBookCommand request, CancellationToken cancellationToken)
    {
        var wantedBooks = await _repository.GetByPredicateAsync<WantedBook>
            (ob => ob.UserId == request.UserId && ob.BookId == request.BookId, cancellationToken);
        if (!wantedBooks.Any()) { throw new Exception($"User {request.UserId} doesn't want the book {request.BookId}."); }

        _repository.Delete(wantedBooks);

        await _repository.SaveAsync(cancellationToken);
    }
}
