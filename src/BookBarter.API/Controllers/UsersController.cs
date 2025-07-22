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
    public Task<UserDto> GetByIdUser(int id, [FromQuery] bool excludeUnapprovedBooks, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetByIdUserQuery { 
            Id = id,
            ExcludeUnapprovedBooks = excludeUnapprovedBooks
        }, cancellationToken);
    }

    [HttpGet]
    [Route("me/chats")]
    public Task<List<MessagingUserDto>> GetUserChats(CancellationToken cancellationToken)
    {
        var query = new GetUserChatsQuery { };
        return _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public Task<PaginatedResult<ListedUserDto>> GetPagedUsers([FromBody] GetPagedUsersQuery getPagedUsersQuery,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(getPagedUsersQuery, cancellationToken);
    }

    [HttpPut]
    [Route("me")]
    public async Task UpdateUser([FromBody] UpdateUserCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task DeleteUser(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteUserCommand { Id = id }, cancellationToken);
    }

    [HttpPost]
    [Route("me/ownedBooks")]
    public async Task AddOwnedBook([FromBody] AddOwnedBookCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPost]
    [Route("me/wantedBooks")]
    public async Task AddWantedBook([FromBody] AddWantedBookCommand command, CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    [Route("me/ownedBooks/{bookId}")]
    public async Task DeleteOwnedBook(int bookId, [FromBody] DeleteOwnedBookCommand command, 
        CancellationToken cancellationToken)
    {
        command.BookId = bookId;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpDelete]
    [Route("me/wantedBooks/{bookId}")]
    public async Task DeleteWantedBook(int bookId, [FromBody] DeleteWantedBookCommand command, 
        CancellationToken cancellationToken)
    {
        command.BookId = bookId;
        await _mediator.Send(command, cancellationToken);
    }
}
