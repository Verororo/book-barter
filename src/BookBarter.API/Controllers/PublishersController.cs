using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Publishers.Commands;
using BookBarter.Application.Publishers.Queries;
using BookBarter.Application.Publishers.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PublishersController : ControllerBase
{
    private readonly IMediator _mediator;
    public PublishersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public Task<PaginatedResult<PublisherDto>> GetPagedPublishers([FromBody] GetPagedPublishersQuery getPagedPublishersQuery,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(getPagedPublishersQuery, cancellationToken);
    }

    [HttpPost]
    [Route("paged/moderated")]
    [Authorize(Roles = "Moderator")]
    public Task<PaginatedResult<PublisherForModerationDto>> GetPagedPublishersForModeration([FromBody] GetPagedPublishersForModerationQuery query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public Task<int?> CreatePublisher(CreatePublisherCommand command, CancellationToken cancellationToken)
    {
        return _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task UpdatePublisher(int id, [FromBody] UpdatePublisherCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}/approve")]
    [Authorize(Roles = "Moderator")]
    public async Task ApprovePublisher(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ApprovePublisherCommand { Id = id }, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task DeletePublisher(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePublisherCommand { Id = id }, cancellationToken);
    }
}
