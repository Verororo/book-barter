
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands.WantedBook;

public class AddWantedBookCommand : IRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
}

public class AddWantedBookCommandHandler : IRequestHandler<AddWantedBookCommand>
{
    private readonly IGenericRepository _repository;
    public AddWantedBookCommandHandler (IGenericRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(AddWantedBookCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync<User>(request.UserId, cancellationToken,
            u => u.OwnedBooks, u => u.WantedBooks);
        if (user == null) throw new EntityNotFoundException(typeof(User).Name, request.UserId);

        var book = await _repository.GetByIdAsync<Book>(request.BookId, cancellationToken);
        if (book == null) throw new EntityNotFoundException(typeof(Book).Name, request.BookId);

        var existingOwnedBook = user.OwnedBooks.Any(ob => ob.BookId == request.BookId);
        if (existingOwnedBook)
        {
            throw new Exception($"Book {request.BookId} is already owned by user {request.UserId}.");
        }

        var existingWantedBook = user.WantedBooks.Any(b => b.Id == request.BookId);
        if (existingWantedBook)
        {
            throw new Exception($"Book {request.BookId} is already wanted by user {request.UserId}.");
        }

        user.WantedBooks.Add(book);

        await _repository.SaveAsync(cancellationToken);
    }
}
