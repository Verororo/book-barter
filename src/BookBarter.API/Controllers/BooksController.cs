
using BookBarter.Application.Authors.Commands;
using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
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
    [AllowAnonymous]
    public async Task<BookDto> GetByIdBook(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new GetByIdBookQuery { Id = id }, cancellationToken);
        return response;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<BookDto>> GetPagedBooks([FromBody] GetPagedBooksQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return response;
    }

    [HttpPost]
    [Route("paged/moderated")]
    [AllowAnonymous]
    public async Task<PaginatedResult<BookForModerationDto>> GetPagedBooksForModeration([FromBody] GetPagedBooksForModerationQuery query,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(query, cancellationToken);
        return response;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<int> CreateBook([FromBody] CreateBookCommand command, 
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return response;
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
    [AllowAnonymous]
    public async Task ApproveBook(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new ApproveBookCommand { Id = id }, cancellationToken);
    }

    [HttpDelete]
    [Route("{id}")]
    [AllowAnonymous]
    //[Authorize(Roles = "Moderator")]
    public async Task DeleteBook(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);
    }
}
