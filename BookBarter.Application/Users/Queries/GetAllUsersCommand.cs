
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetAllUsersCommand() : IRequest<List<User>>;

public class GetAllUsersHandler : IRequestHandler<GetAllUsersCommand, List<User>>
{
    private readonly IReadingRepository<User> _userRepository;

    public GetAllUsersHandler(IReadingRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<List<User>> Handle(GetAllUsersCommand request, CancellationToken cancellationToken)
    {
        return await _userRepository.GetAllAsync();
    }
}
