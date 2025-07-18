using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly IMediator _mediator;
    public BooksController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public Task<BookDto> GetByIdBook(int id, CancellationToken cancellationToken)
    {
        return _mediator.Send(new GetByIdBookQuery { Id = id }, cancellationToken);
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public Task<PaginatedResult<BookDto>> GetPagedBooks([FromBody] GetPagedBooksQuery query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [Route("paged/moderated")]
    [Authorize(Roles = "Moderator")]
    public Task<PaginatedResult<BookForModerationDto>> GetPagedBooksForModeration([FromBody] GetPagedBooksForModerationQuery query,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(query, cancellationToken);
    }

    [HttpPost]
    [AllowAnonymous]
    public Task<int> CreateBook([FromBody] CreateBookCommand command, 
        CancellationToken cancellationToken)
    {
        return _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task UpdateBook(int id, [FromBody] UpdateBookCommand command,
        CancellationToken cancellationToken)
    {
        command.Id = id;
        await _mediator.Send(command, cancellationToken);
    }

    [HttpPut]
    [Route("{id}/approve")]
    [Authorize(Roles = "Moderator")]
    public async Task ApproveBook(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ApproveBookCommand { Id = id }, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task DeleteBook(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);
    }
}
