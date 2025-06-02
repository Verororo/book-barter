
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Users.Commands;
public record UpdateUserCommand(User user) : IRequest;
public class UpdateUserHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IRepository<User> _userRepository;
    public UpdateUserHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.user.Id);
        if (user == null)
        {
            throw new EntityNotFoundException($"User not found by id: {request.user.Id}");
        }

        user = request.user;
        await _userRepository.SaveAsync();
    }
}
