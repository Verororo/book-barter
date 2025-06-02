
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record DeleteUserCommand(int id) : IRequest;
public class DeleteUserHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly IRepository<User> _userRepository;
    public DeleteUserHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.id);
        if (user == null)
        {
            throw new EntityNotFoundException($"User with id {request.id} has not been found");
        }

        _userRepository.Delete(user);
        await _userRepository.SaveAsync();
    }
}
