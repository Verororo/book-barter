using BookBarter.Application.Abstractions;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record DeleteUserHasBook(int userId, int bookId) : IRequest<Unit>;
public class DeleteUserBookHandler : IRequestHandler<DeleteUserHasBook, Unit>
{
    private readonly IUserHasBookRepository _userBookRepository;
    public DeleteUserBookHandler(IUserHasBookRepository userBookRepository)
    {
        _userBookRepository = userBookRepository;
    }
    public Task<Unit> Handle(DeleteUserHasBook request, CancellationToken cancellationToken)
    {
        var userBook = _userBookRepository.GetByPredicate(x => x.UserId == request.userId &&
                                                            x.BookId == request.bookId)[0];
        if (userBook == null)
        {
            throw new RepoMemberAbsentException($"User-Book relation {request.userId} - {request.bookId}" +
                $"has not been found");
        }
        _userBookRepository.Delete(userBook);
        return Task.FromResult(Unit.Value);
    }
}
