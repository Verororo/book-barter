
using BookBarter.Application.Abstractions;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands;
public record CreateOwnedBookCommand(int userId, int bookId, int bookStateId) : IRequest;
public class CreateOwnedBookHandler : IRequestHandler<CreateOwnedBookCommand>
{
    private readonly IRepository<OwnedBook> _ownedBookRepository;
    private readonly IReadingRepository<User> _userRepository;
    private readonly IReadingRepository<Book> _bookRepository;
    private readonly IReadingRepository<BookState> _bookStateRepository;
    public CreateOwnedBookHandler(
        IRepository<OwnedBook> ownedBookRepository,
        IReadingRepository<User> userRepository,
        IReadingRepository<Book> bookRepository,
        IReadingRepository<BookState> bookStateRepository)
    {
        _ownedBookRepository = ownedBookRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
        _bookStateRepository = bookStateRepository;
    }
    public async Task Handle(CreateOwnedBookCommand request, CancellationToken cancellationToken)
    {
        // what if within the same business logic there is a need to do independent read and write operations?
        // use the same one repository

        // .GetById() is better for further working with retrieved objects

        // TODO: write a separate validator service to replace first two '== null' checks
        // It is possible to place in into a 'Common' dir in the root of the Application Layer
        bool userFound = await _userRepository.ExistsByIdAsync(request.userId);
        bool bookFound = await _bookRepository.ExistsByIdAsync(request.bookId);
        bool bookStateFound = await _bookStateRepository.ExistsByIdAsync(request.bookStateId);

        if (!userFound)
        {
            throw new EntityNotFoundException($"User not found by id: {request.userId}");
        }
        if (!bookFound)
        {
            throw new EntityNotFoundException($"Book not found by id: {request.bookId}");
        }
        if (!bookStateFound)
        {
            throw new EntityNotFoundException($"Book state not found by id: {request.bookStateId}");
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

        User? user = await _userRepository.GetByIdAsync(request.userId);
        Book? book = await _bookRepository.GetByIdAsync(request.bookId);
        BookState? bookState = await _bookStateRepository.GetByIdAsync(request.bookStateId);

        if (user == null || book == null || bookState == null)
        {
            throw new Exception($"Unknown error: trying to retrieve an object from the database returned NULL");
        }

        var ownedBook = new OwnedBook 
        { 
            Book = book, 
            User = user, 
            BookState = bookState
        };

        _ownedBookRepository.Add(ownedBook);
        await _ownedBookRepository.SaveAsync();
    }
}
