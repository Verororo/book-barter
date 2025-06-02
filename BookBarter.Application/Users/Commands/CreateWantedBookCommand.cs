
using BookBarter.Application.Abstractions;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands;
public record CreateWantedBookCommand(int userId, int bookId) : IRequest;
public class CreateWantedBookHandler : IRequestHandler<CreateWantedBookCommand>
{
    private readonly IRepository<User> _userRepository;
    private readonly IReadingRepository<Book> _bookRepository;
    private readonly IReadingRepository<OwnedBook> _ownedBookRepository;
    public CreateWantedBookHandler(
        IRepository<User> userRepository,
        IReadingRepository<Book> bookRepository,
        IReadingRepository<OwnedBook> ownedBookRepository)
    {
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _ownedBookRepository = ownedBookRepository;
    }
    public async Task Handle(CreateWantedBookCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(request.userId);
        Book? book = await _bookRepository.GetByIdAsync(request.bookId);


        if (user == null)
        {
            throw new EntityNotFoundException($"User not found by id: {request.userId}");
        }
        if (book == null)
        {
            throw new EntityNotFoundException($"Book not found by id: {request.bookId}");
        }
        if ((await _ownedBookRepository.GetByPredicateAsync
            (ob => ob.UserId == request.userId && ob.BookId == request.bookId)).Any())
        {
            throw new BusinessLogicException($"User with id {request.userId} already owns book {request.bookId}");
        }
        if ((await _userRepository.GetByPredicateAsync
            (u => u.Id == request.userId && u.WantedBooks.Any(b => b.Id == request.bookId))).Any())
        {
            throw new BusinessLogicException($"User with id {request.userId} has already specified book" +
                $"{request.bookId} as wanted");
        }

        user.WantedBooks.Add(book);
        await _userRepository.SaveAsync();
    }
}
