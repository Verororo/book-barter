
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands;
public record UpdateBook(int id, string name, string email, string city) : IRequest<UserDto>;
public class UpdateUserHandler : IRequestHandler<UpdateBook, UserDto>
{
    private readonly IUserRepository _userRepository;
    public UpdateUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<UserDto> Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        var oldUser = _userRepository.GetById(request.id);
        if (oldUser == null)
        {
            throw new RepoMemberAbsentException($"User with id {request.id} has not been found");
        }
        var newUser = new User(request.id, request.name, request.email, request.city);
        var updatedUser = _userRepository.Update(newUser);
        return Task.FromResult(UserDto.FromUser(updatedUser));
    }
}
