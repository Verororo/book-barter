
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
using MediatR;

namespace UserBarter.Application.Users.Queries;

public class GetPagedUsersQuery : PagedQuery, IRequest<PaginatedResult<ListedUserDto>>
{
    public string? UserName { get; set; }
    public int? CityId { get; set; }
    public ICollection<int>? OwnedBooksIds { get; set; }
    public ICollection<int>? WantedBooksIds { get; set; }

    public ICollection<int>? OwnedBookAuthorsIds { get; set; }
    public int? OwnedBookGenreId { get; set; }
    public int? OwnedBookPublisherId { get; set; }

    public ICollection<int>? WantedBookAuthorsIds { get; set; }
    public int? WantedBookGenreId { get; set; }
    public int? WantedBookPublisherId { get; set; }
}

public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, PaginatedResult<ListedUserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetPagedUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PaginatedResult<ListedUserDto>> Handle(GetPagedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetDtoPagedAsync<ListedUserDto>(request, cancellationToken);

        return result;
    }
}

