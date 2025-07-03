
using BookBarter.Application.Common.Interfaces.Repositories;
using MediatR;
using BookBarter.Domain.Exceptions;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Commands.WantedBook;

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
        var user = await _repository.GetByIdAsync<User>(request.UserId, cancellationToken,
            u => u.WantedBooks);
        if (user == null) { throw new EntityNotFoundException(typeof(User).Name, request.UserId); }

        var book = user.WantedBooks.FirstOrDefault(b => b.Id == request.BookId);
        if (book == null) { throw new Exception($"User {user.Id} doesn't want the book {request.BookId}."); }

        user.WantedBooks.Remove(book);

        await _repository.SaveAsync(cancellationToken);
    }
}
