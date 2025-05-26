
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record DeleteUser(int id) : IRequest<Unit>;
public class DeleteUserHandler : IRequestHandler<DeleteUser, Unit>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public Task<Unit> Handle(DeleteUser request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetById(request.id);
        if (user == null)
        {
            throw new RepoMemberAbsentException($"User with id {request.id} has not been found");
        }
        _userRepository.Delete(user);
        return Task.FromResult(Unit.Value);
    }
}
