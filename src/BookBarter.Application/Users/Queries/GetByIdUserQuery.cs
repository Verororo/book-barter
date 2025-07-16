
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
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
    public GetByIdUserQueryHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserDto> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
    {
        var userDto = await _userRepository.GetDtoByIdAsync<UserDto>(request.Id, request.ExcludeUnapprovedBooks, cancellationToken);
        if (userDto == null) throw new EntityNotFoundException(typeof(User).Name, request.Id);

        return userDto;
    }
}