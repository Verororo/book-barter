using BookBarter.Application.Common.Models;
using BookBarter.Application.Messages.Commands;
using BookBarter.Application.Messages.Queries;
using BookBarter.Application.Messages.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class MessagesController : ControllerBase
{
    private readonly IMediator _mediator;
    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("paged")]
    public Task<PaginatedResult<MessageDto>> GetPagedMessages([FromBody] GetPagedMessagesQuery query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }
}
