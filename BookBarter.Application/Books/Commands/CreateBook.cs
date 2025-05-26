
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Application.Books.Responses;

namespace BookBarter.Application.Books.Commands;
public record CreateBook(string isbn, string name, string genre) : IRequest<BookDto>;
public class CreateBookHandler : IRequestHandler<CreateBook, BookDto>
{
    private readonly IBookRepository _bookRepository;
    public CreateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public Task<BookDto> Handle(CreateBook request, CancellationToken cancellationToken)
    {
        var book = new Book(GetNextId(), request.isbn, request.name, request.genre);
        var createdBook = _bookRepository.Create(book);
        return Task.FromResult(BookDto.FromBook(createdBook));
    }
    private int GetNextId()
    {
        return _bookRepository.GetNextId();
    }
}
