using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Services;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.BookBooks.Commands;
public class DeleteBookCommand : IRequest
{
    public int Id { get; set; }
}
public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IWritingRepository<Book> _bookRepository;
    public DeleteBookCommandHandler(IWritingRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.Id, cancellationToken);
        if (book == null) throw new EntityNotFoundException(typeof(Book).Name, request.Id);

        _bookRepository.Delete(book);
        await _bookRepository.SaveAsync(cancellationToken);
    }
}
