using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Messages.Commands;
using BookBarter.Application.Messages.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace BookBarter.API.Hubs;

[Authorize]
public class MessageHub : Hub
{
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IMediator _mediator;

    public MessageHub(ICurrentUserProvider currentUserProvider, IMediator mediator)
    {
        _currentUserProvider = currentUserProvider;
        _mediator = mediator;
    }

    public async Task SendMessage(CreateMessageCommand command)
    {
        var cancellationToken = Context.ConnectionAborted;

        var messageId = await _mediator.Send(command, cancellationToken);

        var messageDto = new MessageDto
        {
            Id = messageId,
            SenderId = _currentUserProvider.UserId!.Value,
            ReceiverId = command.ReceiverId,
            Body = command.Body,
            SentTime = DateTime.UtcNow
        };

        // Send to receiver
        await Clients.User(command.ReceiverId.ToString()).SendAsync("ReceiveMessage", messageDto, cancellationToken);

        // Also send to sender to confirm the message
        await Clients.Caller.SendAsync("ReceiveMessage", messageDto, cancellationToken);
    }
}