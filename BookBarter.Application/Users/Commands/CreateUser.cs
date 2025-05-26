
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Application.Users.Responses;

namespace BookBarter.Application.Users.Commands;
public record CreateUser(string name, string email, string city) : IRequest<UserDto>;
public class CreateUserHandler : IRequestHandler<CreateUser, UserDto>
{
    private readonly IUserRepository _userRepository;
    public CreateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<UserDto> Handle(CreateUser request, CancellationToken cancellationToken)
    {
        var user = new User(GetNextId(), request.name, request.email, request.city);
        var createdUser = _userRepository.Create(user);
        return Task.FromResult(UserDto.FromUser(createdUser));
    }

    private int GetNextId()
    {
        return _userRepository.GetNextId();
    }
}
