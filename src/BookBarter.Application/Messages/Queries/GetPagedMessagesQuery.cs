
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Messages.Responses;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Messages.Queries;

public class GetPagedMessagesQuery : PagedQuery, IRequest<PaginatedResult<MessageDto>>
{
    public int CollocutorId { get; set; }
}

public class GetPagedMessagesQueryHandler : IRequestHandler<GetPagedMessagesQuery, PaginatedResult<MessageDto>>
{
    private readonly IMessageRepository _messageRepository;
    private readonly ICurrentUserProvider _currentUserProvider;

    public GetPagedMessagesQueryHandler(IMessageRepository messageRepository, ICurrentUserProvider currentUserProvider)
    {
        _messageRepository = messageRepository;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<PaginatedResult<MessageDto>> Handle(GetPagedMessagesQuery request,
        CancellationToken cancellationToken)
    {
        if (!_currentUserProvider.UserId.HasValue)
        {
            throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
        }
        var userId = _currentUserProvider.UserId.Value;

        var result = await _messageRepository.GetDtoPagedAsync(userId, request, cancellationToken);

        return result;
    }
}
