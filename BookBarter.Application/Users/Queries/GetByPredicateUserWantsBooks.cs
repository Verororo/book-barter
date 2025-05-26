using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByPredicateUserWantsBooks(Func<UserWantsBook, bool> predicate)
    : IRequest<List<UserWantsBookDto>>;

public class GetByPredicateWantedUserBooksHandler
    : IRequestHandler<GetByPredicateUserWantsBooks, List<UserWantsBookDto>>
{
    private readonly IUserWantsBookRepository _wantedUserBookRepository;

    public GetByPredicateWantedUserBooksHandler(IUserWantsBookRepository wantedUserBookRepository)
    {
        _wantedUserBookRepository = wantedUserBookRepository;
    }

    public Task<List<UserWantsBookDto>> Handle(GetByPredicateUserWantsBooks request, 
        CancellationToken cancellationToken)
    {
        var WantedUserBookList = _wantedUserBookRepository.GetByPredicate(request.predicate);
        return Task.FromResult(WantedUserBookList.Select(UserWantsBookDto.FromWantedUserBook).ToList());
    }
}
