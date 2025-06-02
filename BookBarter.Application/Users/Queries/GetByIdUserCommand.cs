
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByIdUserCommand(int userId) : IRequest<User>;

public class GetByIdUserHandler : IRequestHandler<GetByIdUserCommand, User>
{
    private readonly IReadingRepository<User> _userRepository;

    public GetByIdUserHandler(IReadingRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> Handle(GetByIdUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.userId);
        if (user == null)
        {
            throw new EntityNotFoundException($"User not found by id: {request.userId}");
        }

        return user;
    }
}
