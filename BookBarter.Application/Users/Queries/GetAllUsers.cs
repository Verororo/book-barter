
using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllUsers() : IRequest<List<UserDto>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsers, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetAllUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<UserDto>> Handle(GetAllUsers request, CancellationToken cancellationToken)
    {
        var userList = _userRepository.GetAll();
        return Task.FromResult(userList.Select(UserDto.FromUser).ToList());
    }
}
