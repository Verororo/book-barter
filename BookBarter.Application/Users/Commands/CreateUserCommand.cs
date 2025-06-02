
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Commands;
public record CreateUserCommand(string name, string email, string city) : IRequest<User>;
public class CreateUserHandler : IRequestHandler<CreateUserCommand, User>
{
    private readonly IRepository<User> _userRepository;
    public CreateUserHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User 
        {
            Name = request.name,
            Email = request.email,
            City = request.city,
            RegistrationDate = DateTime.UtcNow
        };

        _userRepository.Add(user);
        await _userRepository.SaveAsync();

        return user;
    }
}
