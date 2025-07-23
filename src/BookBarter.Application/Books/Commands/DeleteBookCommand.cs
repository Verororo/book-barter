using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.BookBooks.Commands;
public class DeleteBookCommand : IRequest
{
    public int Id { get; set; }
}
public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public DeleteBookCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }
    public async Task Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync<Book>(request.Id, cancellationToken, b => b.Publisher, b => b.Authors);
        _entityExistenceValidator.ValidateAsync(book, request.Id);

        if (!book.Publisher.Approved)
        {
            _repository.Delete(book.Publisher);
        }

        foreach (var author in book.Authors)
        {
            if (!author.Approved)
            {
                _repository.Delete(author);
            }
        }

        _repository.Delete(book);
        await _repository.SaveAsync(cancellationToken);
    }
}
