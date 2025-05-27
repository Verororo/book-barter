
using BookBarter.Application.Abstractions;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using BookBarter.Application.Users.Responses;

namespace BookBarter.Application.Users.Commands;
public record CreateOwnedBook(int userId, int bookId, int bookStateId) : IRequest<OwnedBookDto>;
public class CreateUserBookHandler : IRequestHandler<CreateOwnedBook, OwnedBookDto>
{
    private readonly IOwnedBookRepository _userBookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    public CreateUserBookHandler(
        IOwnedBookRepository userBookRepository,
        IUserRepository userRepository,
        IBookRepository bookRepository)
    {
        _userBookRepository = userBookRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }
    public Task<OwnedBookDto> Handle(CreateOwnedBook request, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetById(request.userId);
        Book? book = _bookRepository.GetById(request.bookId);

        if (user == null)
        {
            throw new RepoMemberAbsentException($"User {request.userId} doesn't exist");
        }
        if (book == null)
        {
            throw new RepoMemberAbsentException($"Book {request.bookId} doesn't exist");
        }
        if (_userBookRepository.GetByPredicate
            (x => x.UserId == request.userId && x.BookId == request.bookId).Any())
        {
            throw new BusinessLogicException($"User {request.userId} already has book {request.bookId}");
        }
        var userBook = new OwnedBook { BookId = request.bookId, UserId = request.userId, BookStateId = request.bookStateId};
        var createdUserBook = _userBookRepository.Create(userBook);
        return Task.FromResult(OwnedBookDto.FromUserBook(createdUserBook));
    }
}
