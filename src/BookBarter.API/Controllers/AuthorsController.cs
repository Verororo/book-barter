using BookBarter.Application.Authors.Commands;
using BookBarter.Application.Authors.Queries;
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
    public async Task<PaginatedResult<AuthorDto>> GetPagedAuthors([FromBody] GetPagedAuthorsQuery getPagedAuthorsQuery,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(getPagedAuthorsQuery, cancellationToken);
        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task CreateAuthor(CreateAuthorCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }
    /*
    [HttpDelete]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task DeleteAuthor(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteAuthorCommand { Id = id }, cancellationToken);
    }
    */
}
