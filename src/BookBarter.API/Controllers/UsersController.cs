using BookBarter.Application.Books.Commands;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Commands;
using BookBarter.Application.Users.Commands.OwnedBooks;
using BookBarter.Application.Users.Commands.WantedBooks;
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
    [AllowAnonymous]
    public async Task<UserDto> GetByIdUser(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetByIdUserQuery { Id = id }, cancellationToken);
        return response;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<ListedUserDto>> GetPagedUsers([FromBody] GetPagedUsersQuery getPagedUsersQuery,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(getPagedUsersQuery, cancellationToken);
        return response;
    }

    [HttpPut]
    [Route("me")]
    [AllowAnonymous]
    public async Task UpdateUser([FromBody] UpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
    }

    [HttpPost]
    [Route("me/ownedBooks")]
    [AllowAnonymous]
    public async Task AddOwnedBook([FromBody] AddOwnedBookCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPost]
    [Route("me/wantedBooks")]
    [AllowAnonymous]
    public async Task AddWantedBook([FromBody] AddWantedBookCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    [Route("me/ownedBooks/{bookId}")]
    [AllowAnonymous]
    public async Task DeleteOwnedBook(int bookId, [FromBody] DeleteOwnedBookCommand command, 
        CancellationToken cancellationToken)
    {
        command.BookId = bookId;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    [Route("me/wantedBooks/{bookId}")]
    [AllowAnonymous]
    public async Task DeleteWantedBook(int bookId, [FromBody] DeleteWantedBookCommand command, 
        CancellationToken cancellationToken)
    {
        command.BookId = bookId;
        await _mediator.Send(command, cancellationToken);
    }
}
