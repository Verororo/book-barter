
using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
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
    public async Task<BookDto> GetByIdBook(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetByIdBookQuery { Id = id }, cancellationToken);
        return response;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<BookDto>> GetPagedBooks([FromBody] GetPagedBooksQuery getPagedBooksQuery,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(getPagedBooksQuery, cancellationToken);
        return response;
    }

    [HttpPost]
    // [Authorize(Roles = "User")]
    [AllowAnonymous]
    public async Task CreateBook([FromBody] CreateBookCommand command, 
        CancellationToken cancellationToken)
    {
        await _mediator.Send(command, cancellationToken);
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

    [HttpDelete]
    [Route("{id}")]
    [Authorize(Roles = "Moderator")]
    public async Task DeleteBook(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);
    }
}
