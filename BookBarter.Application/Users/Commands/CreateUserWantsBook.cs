using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record CreateUserWantsBook(int userId, int bookId) : IRequest<UserWantsBookDto>;
public class CreateWantedUserBookHandler : IRequestHandler<CreateUserWantsBook, UserWantsBookDto>
{
    private readonly IUserWantsBookRepository _wantedUserBookRepository;
    private readonly IUserHasBookRepository _userBookRepository;
    private readonly IUserRepository _userRepository;
    public CreateWantedUserBookHandler(IUserWantsBookRepository wantedUserBookRepository,
        IUserHasBookRepository userBookRepository,
        IUserRepository userRepository)
    {
        _wantedUserBookRepository = wantedUserBookRepository;
        _userBookRepository = userBookRepository;
        _userRepository = userRepository;
    }
    public Task<UserWantsBookDto> Handle(CreateUserWantsBook request, CancellationToken cancellationToken)
    {
        User? user = _userRepository.GetById(request.userId);
        List<UserHasBook> heldBooks = _userBookRepository.GetByPredicate(x => x.BookId == request.bookId);

        if (user == null)
        {
            throw new RepoMemberAbsentException($"User {request.userId} doesn't exist");
        }
        if (heldBooks.Count == 0)
        {
            throw new RepoMemberAbsentException($"Book {request.bookId} isn't held by any user");
        }
        if (heldBooks.Select(x => x.UserId).Contains(request.userId))
        {
            throw new BusinessLogicException($"User {request.userId} already has book {request.bookId}");
        }
        if (_wantedUserBookRepository.GetByPredicate
            (x => x.UserId == request.userId && x.BookId == request.bookId).Any())
        {
            throw new BusinessLogicException($"User {request.userId} has already specified " +
                $"book {request.bookId} as wanted");
        }
        
        var wantedUserBook = new UserWantsBook { UserId = request.userId, BookId = request.bookId };
        var createdWantedUserBook = _wantedUserBookRepository.Create(wantedUserBook);
        return Task.FromResult(UserWantsBookDto.FromWantedUserBook(createdWantedUserBook));
    }
}
