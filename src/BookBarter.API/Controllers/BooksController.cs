
using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// divide exceptions by critical and invalid user outputs
// (decided by how the frontend handles them)

namespace BookBarter.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : ControllerBase // custom ControllerBase? (see sample project)
    {
        private readonly IMediator _mediator;
        public BooksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}")]
        // IActionResult -> BookDto / ActionResult<BookDto>
        public async Task<BookDto> GetBookById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdBookQuery { Id = id }, cancellationToken);
            return response;
        }

        [HttpPost]
        public async Task CreateBook([FromBody] CreateBookCommand command, 
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
        }

        [HttpPut]
        public async Task UpdateBook(int id, [FromBody] UpdateBookCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task DeleteBook(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);
        }
    }
}
