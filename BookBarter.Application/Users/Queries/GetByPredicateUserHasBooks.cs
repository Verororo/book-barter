using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByPredicateUserHasBooks(Func<UserHasBook, bool> predicate) : IRequest<List<UserHasBookDto>>;

public class GetByPredicateUserBooksHandler : IRequestHandler<GetByPredicateUserHasBooks, List<UserHasBookDto>>
{
    private readonly IUserHasBookRepository _userBookRepository;

    public GetByPredicateUserBooksHandler(IUserHasBookRepository userBookRepository)
    {
        _userBookRepository = userBookRepository;
    }

    public Task<List<UserHasBookDto>> Handle(GetByPredicateUserHasBooks request, CancellationToken cancellationToken)
    {
        var userBookList = _userBookRepository.GetByPredicate(request.predicate);
        return Task.FromResult(userBookList.Select(UserHasBookDto.FromUserBook).ToList());
    }
}
