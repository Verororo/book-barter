
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Queries;

public class GetUserChatsQuery : IRequest<List<MessagingUserDto>> { }

public class GetUserChatsQueryHandler : IRequestHandler<GetUserChatsQuery, List<MessagingUserDto>>
{
    private readonly IUserRepository _userRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetUserChatsQueryHandler(IUserRepository userRepository, ICurrentUserProvider currentUserProvider)
    {
        _userRepository = userRepository;
        _currentUserProvider = currentUserProvider;
    }

    public Task<List<MessagingUserDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUserProvider.UserId.HasValue)
        {
            throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
        }
        var userId = _currentUserProvider.UserId.Value;

        return _userRepository.GetUserChatsAsync(userId, cancellationToken);
    }
}