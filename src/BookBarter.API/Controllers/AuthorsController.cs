using BookBarter.Application.Authors.Commands;
using BookBarter.Application.Authors.Queries;
using BookBarter.Application.Authors.Responses;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class AuthorsController : ControllerBase
{
    private readonly IMediator _mediator;
    public AuthorsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public Task<PaginatedResult<AuthorDto>> GetPagedAuthors([FromBody] GetPagedAuthorsQuery query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [Route("paged/moderated")]
    [Authorize(Roles = "Moderator")]
    public Task<PaginatedResult<AuthorForModerationDto>> GetPagedAuthorsForModeration([FromBody] GetPagedAuthorsForModerationQuery query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    public Task<int> CreateAuthor(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        return _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}/approve")]
    [Authorize(Roles = "Moderator")]
    public async Task ApproveAuthor(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ApproveAuthorCommand { Id = id }, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAuthorCommand { Id = id }, cancellationToken);
    }
}
