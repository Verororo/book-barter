using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands.OwnedBooks;

public class AddOwnedBookCommand : IRequest
{
    public int UserId { get; set; }
    public int BookId { get; set; }
    public int BookStateId { get; set; }
}

public class AddOwnedBookCommandHandler : IRequestHandler<AddOwnedBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _existenceValidator;
    public AddOwnedBookCommandHandler(IGenericRepository repository, 
        IEntityExistenceValidator existenceValidator)
    {
        _repository = repository;
        _existenceValidator = existenceValidator;
    }
    public async Task Handle(AddOwnedBookCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync<User>(request.UserId, cancellationToken,
            u => u.OwnedBooks, u => u.WantedBooks);
        if (user == null) throw new EntityNotFoundException(typeof(User).Name, request.UserId);

        await _existenceValidator.ValidateAsync<Book>(request.BookId, cancellationToken);
        await _existenceValidator.ValidateAsync<BookState>(request.BookStateId, cancellationToken);

        var existingOwnedBook = user.OwnedBooks.Any(ob => ob.BookId == request.BookId);
        if (existingOwnedBook)
        {
            throw new Exception($"Book {request.BookId} is already owned by User {request.UserId}.");
        }

        var existingWantedBook = user.WantedBooks.Any(b => b.Id == request.BookId);
        if (existingWantedBook)
        {
            throw new Exception($"Book {request.BookId} is already wanted by User {request.UserId}.");
        }

        var newOwnedBook = new OwnedBook
        {
            User = user,
            BookId = request.BookId,
            BookStateId = request.BookStateId
        };

        user.OwnedBooks.Add(newOwnedBook);

        await _repository.SaveAsync(cancellationToken);
    }
}