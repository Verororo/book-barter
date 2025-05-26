using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllUserHasBooks() : IRequest<List<UserHasBookDto>>;

public class GetAllUserBooksHandler : IRequestHandler<GetAllUserHasBooks, List<UserHasBookDto>>
{
    private readonly IUserHasBookRepository _userBookRepository;

    public GetAllUserBooksHandler(IUserHasBookRepository userBookRepository)
    {
        _userBookRepository = userBookRepository;
    }

    public Task<List<UserHasBookDto>> Handle(GetAllUserHasBooks request, CancellationToken cancellationToken)
    {
        var userBookList = _userBookRepository.GetAll();
        return Task.FromResult(userBookList.Select(UserHasBookDto.FromUserBook).ToList());
    }
}
