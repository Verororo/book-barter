using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Users.Queries;
using BookBarter.Application.Users.Responses;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserBarter.Application.Users.Queries;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IMediator _mediator;
    public UsersController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<UserDto> GetByIdUser(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetByIdUserQuery { Id = id }, cancellationToken);
        return response;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<IActionResult> GetPagedUsers([FromBody] GetPagedUsersQuery getPagedUsersQuery,
    CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(getPagedUsersQuery, cancellationToken);
        return Ok(result);
    }
}
