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
    private readonly IGenericRepository _repository;
    public DeleteBookCommandHandler(IGenericRepository repository)
    {
        _repository = repository;
    }
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync<Book>(request.Id, cancellationToken, b => b.Publisher, b => b.Authors);
        if (book == null) throw new EntityNotFoundException(typeof(Book).Name, request.Id);

        if (!book.Approved)
        {
            if (!book.Publisher.Approved)
            {
                _repository.Delete<Publisher>(book.Publisher);
            }

            foreach (var author in book.Authors)
            {
                if (!author.Approved)
                {
                    _repository.Delete<Author>(author);
                }
            }
        }

        _repository.Delete<Book>(book);
        await _repository.SaveAsync(cancellationToken);
    }
}
