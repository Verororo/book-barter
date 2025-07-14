using BookBarter.Application.Authors.Commands;
using BookBarter.Application.Authors.Queries;
using BookBarter.Application.Authors.Responses;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
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
    /*
    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task<UserDto> GetByIdAuthor(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetByIdAuthorQuery { Id = id }, cancellationToken);
        return response;
    }
    */
    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<AuthorDto>> GetPagedAuthors([FromBody] GetPagedAuthorsQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return response;
    }

    [HttpPost]
    [Route("paged/moderated")]
    [AllowAnonymous]
    public async Task<PaginatedResult<AuthorForModerationDto>> GetPagedAuthorsForModeration([FromBody] GetPagedAuthorsForModerationQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<int> CreateAuthor(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return response;
    }

    [HttpPut]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task UpdateAuthor(int id, [FromBody] UpdateAuthorCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}/approve")]
    [AllowAnonymous]
    public async Task ApproveAuthor(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ApproveAuthorCommand { Id = id }, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAuthorCommand { Id = id }, cancellationToken);
    }
}
