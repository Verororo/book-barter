
using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByIdUsers(int id) : IRequest<UserDto>;

public class GetByIdUsersHandler : IRequestHandler<GetByIdUsers, UserDto>
{
    private readonly IUserRepository _userRepository;

    public GetByIdUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<UserDto> Handle(GetByIdUsers request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetById(request.id);
        if (user == null)
        {
            throw new RepoMemberAbsentException($"User with id {request.id} has not been found");
        }
        return Task.FromResult(UserDto.FromUser(user));
    }
}
