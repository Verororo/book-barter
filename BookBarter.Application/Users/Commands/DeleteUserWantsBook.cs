using BookBarter.Application.Abstractions;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record DeleteUserWantsBook(int userId, int bookId) : IRequest<Unit>;
public class DeleteWantedUserBookHandler : IRequestHandler<DeleteUserWantsBook, Unit>
{
    private readonly IUserWantsBookRepository _wantedUserBookRepository;
    public DeleteWantedUserBookHandler(IUserWantsBookRepository wantedUserBookRepository)
    {
        _wantedUserBookRepository = wantedUserBookRepository;
    }
    public Task<Unit> Handle(DeleteUserWantsBook request, CancellationToken cancellationToken)
    {
        var wantedUserBook = _wantedUserBookRepository.GetByPredicate(x => x.UserId == request.userId &&
                                                            x.BookId == request.bookId)[0];
        if (wantedUserBook == null)
        {
            throw new RepoMemberAbsentException($"User-Wanted Book relation {request.userId} - {request.bookId}" +
                $"has not been found");
        }
        _wantedUserBookRepository.Delete(wantedUserBook);
        return Task.FromResult(Unit.Value);
    }
}
