
using System;
using BookBarter.Application.Abstractions;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public record GetByPredicateUsers(Func<User, bool> predicate) : IRequest<List<UserDto>>;

public class GetByPredicateUsersHandler : IRequestHandler<GetByPredicateUsers, List<UserDto>>
{
    private readonly IUserRepository _userRepository;

    public GetByPredicateUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<UserDto>> Handle(GetByPredicateUsers request, CancellationToken cancellationToken)
    {
        var userList = _userRepository.GetByPredicate(request.predicate);
        return Task.FromResult(userList.Select(UserDto.FromUser).ToList());
    }
}
