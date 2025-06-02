
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.BookBooks.Commands;
public record DeleteBookCommand(int id) : IRequest;
public class DeleteBookHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    public DeleteBookHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.id);
        if (book == null)
        {
            throw new EntityNotFoundException($"Book with id {request.id} has not been found");
        }

        _bookRepository.Delete(book);
        await _bookRepository.SaveAsync();
    }
}
