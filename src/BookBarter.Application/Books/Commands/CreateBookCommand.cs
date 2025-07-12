using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Application.Common.Interfaces;

namespace BookBarter.Application.Books.Commands;
public class CreateBookCommand : IRequest<int>
{
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public List<int> AuthorsIds { get; set; } = default!;
    public int GenreId { get; set; }
    public int PublisherId { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, int>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _existenceValidator;
    public CreateBookCommandHandler(
        IGenericRepository repository,
        IEntityExistenceValidator existenceValidator
        )
    {
        _repository = repository;
        _existenceValidator = existenceValidator;
    }
    public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _existenceValidator.ValidateAsync<Genre>(request.GenreId, cancellationToken); 
        await _existenceValidator.ValidateAsync<Publisher>(request.PublisherId, cancellationToken);
        await _existenceValidator.ValidateAsync<Author>(request.AuthorsIds, cancellationToken);

        var existingAuthors = await _repository.GetByPredicateAsync<Author>
            (a => request.AuthorsIds.Contains(a.Id), cancellationToken);

        var book = new Book
        {
            Isbn = request.Isbn,
            Title = request.Title,
            PublicationDate = request.PublicationDate,
            AddedToDatabaseDate = DateTime.UtcNow,
            GenreId = request.GenreId,
            PublisherId = request.PublisherId,
            Authors = existingAuthors
        };

        _repository.Add<Book>(book);
        await _repository.SaveAsync(cancellationToken);

        return book.Id;
    }
}
