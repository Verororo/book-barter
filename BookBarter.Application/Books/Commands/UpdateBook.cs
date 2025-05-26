
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Books.Commands;
public record UpdateBook(int id, string isbn, string name, string email) : IRequest<BookDto>;
public class UpdateBookHandler : IRequestHandler<UpdateBook, BookDto>
{
    private readonly IBookRepository _bookRepository;
    public UpdateBookHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public Task<BookDto> Handle(UpdateBook request, CancellationToken cancellationToken)
    {
        var oldBook = _bookRepository.GetById(request.id);
        if (oldBook == null)
        {
            throw new RepoMemberAbsentException($"Book with id {request.id} has not been found");
        }
        var newBook = new Book(request.id, request.isbn, request.name, request.email);
        var updatedBook = _bookRepository.Update(newBook);
        return Task.FromResult(BookDto.FromBook(updatedBook));
    }
}
