using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllUserWantsBooks() : IRequest<List<UserWantsBookDto>>;

public class GetAllWantedUserBooksHandler : IRequestHandler<GetAllUserWantsBooks, List<UserWantsBookDto>>
{
    private readonly IUserWantsBookRepository _wantedUserBookRepository;

    public GetAllWantedUserBooksHandler(IUserWantsBookRepository wantedUserBookRepository)
    {
        _wantedUserBookRepository = wantedUserBookRepository;
    }

    public Task<List<UserWantsBookDto>> Handle(GetAllUserWantsBooks request, CancellationToken cancellationToken)
    {
        var WantedUserBookList = _wantedUserBookRepository.GetAll();
        return Task.FromResult(WantedUserBookList.Select(UserWantsBookDto.FromWantedUserBook).ToList());
    }
}
