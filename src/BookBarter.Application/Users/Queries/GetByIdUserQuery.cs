
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Users.Responses;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public class GetByIdUserQuery : IRequest<UserDto>
{
    public int Id { get; set; }
    public bool ExcludeUnapprovedBooks { get; set; }
}

public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public GetByIdUserQueryHandler(IUserRepository userRepository, IEntityExistenceValidator entityExistenceValidator)
    {
        _userRepository = userRepository;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var userDto = await _userRepository.GetDtoByIdAsync<UserDto>(request.Id, request.ExcludeUnapprovedBooks, cancellationToken);
        _entityExistenceValidator.ValidateAsync(userDto, request.Id);

        return userDto;
    }
}