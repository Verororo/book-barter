
using BookBarter.Application.Abstractions;
using MediatR;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using BookBarter.Application.Users.Responses;

namespace BookBarter.Application.Users.Commands;
public record CreateUserHasBook(int userId, int bookId, int bookStateId) : IRequest<UserHasBookDto>;
public class CreateUserBookHandler : IRequestHandler<CreateUserHasBook, UserHasBookDto>
{
    private readonly IUserWantsBookRepository _wantedUserBookRepository;
    private readonly IUserHasBookRepository _userBookRepository;
    private readonly IUserRepository _userRepository;
    private readonly IBookRepository _bookRepository;
    public CreateUserBookHandler(IUserWantsBookRepository wantedUserBookRepository,
        IUserHasBookRepository userBookRepository,
        IUserRepository userRepository,
        IBookRepository bookRepository)
    {
        _wantedUserBookRepository = wantedUserBookRepository;
        _userBookRepository = userBookRepository;
        _userRepository = userRepository;
        _bookRepository = bookRepository;
    }
    public Task<UserHasBookDto> Handle(CreateUserHasBook request, CancellationToken cancellationToken)
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
        if (_wantedUserBookRepository.GetByPredicate
            (x => x.UserId == request.userId && x.BookId == request.bookId).Any())
        {
            throw new BusinessLogicException($"User {request.userId} already wants book {request.bookId}");
        }
        if (_userBookRepository.GetByPredicate
            (x => x.UserId == request.userId && x.BookId == request.bookId).Any())
        {
            throw new BusinessLogicException($"User {request.userId} already has book {request.bookId}");
        }
        var userBook = new UserHasBook { BookId = request.bookId, UserId = request.userId, BookStateId = request.bookStateId};
        var createdUserBook = _userBookRepository.Create(userBook);
        return Task.FromResult(UserHasBookDto.FromUserBook(createdUserBook));
    }
}
