
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace UserBarter.Application.Users.Queries;

public class GetPagedUsersQuery : PagedQuery, IRequest<PaginatedResult<UserDto>>
{
    public string? UserName { get; set; }
    public string? City { get; set; }
    public ICollection<int>? OwnedBooksIds { get; set; }
    public ICollection<int>? WantedBooksIds { get; set; }
}

public class GetPagedUsersQueryHandler : IRequestHandler<GetPagedUsersQuery, PaginatedResult<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetPagedUsersQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<PaginatedResult<UserDto>> Handle(GetPagedUsersQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _userRepository.GetDtoPagedAsync(request, cancellationToken);

        return result;
    }
}

