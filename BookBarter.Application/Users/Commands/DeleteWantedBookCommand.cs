using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record DeleteWantedBookCommand(int userId, int bookId) : IRequest;
public class DeleteWantedBookHandler : IRequestHandler<DeleteWantedBookCommand>
{
    private readonly IRepository<User> _userRepository;
    private readonly IReadingRepository<Book> _bookRepository;
    public DeleteWantedBookHandler(IRepository<User> userRepository, IReadingRepository<Book> bookRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }
    public async Task Handle(DeleteWantedBookCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        var book = await _bookRepository.GetByIdAsync(request.bookId);
        if (user == null)
        {
            throw new EntityNotFoundException($"User not found by id: {request.userId}");
        }
        if (book == null)
        {
            throw new EntityNotFoundException($"Book not found by id: {request.bookId}");
        }
        if (!user.WantedBooks.Contains(book))
        {
            throw new EntityNotFoundException($"No user {request.userId} owning a book {request.bookId} " +
                $"pair found");
        }

        bool succesful = user.WantedBooks.Remove(book);
        if (!succesful)
        {
            throw new Exception($"Failed to remove book {request.bookId} from user {request.userId}'s wanted");
        }

        await _userRepository.SaveAsync();
    }
}
