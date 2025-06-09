
using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> GetBookById(int id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(new GetByIdBookQuery { Id = id }, cancellationToken);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookCommand command, 
            CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] UpdateBookCommand command,
            CancellationToken cancellationToken)
        {
            command.Id = id;
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(int id, CancellationToken cancellationToken)
        {
            await _mediator.Send(new DeleteBookCommand { Id = id }, cancellationToken);
            return Ok();
        }
    }
}
