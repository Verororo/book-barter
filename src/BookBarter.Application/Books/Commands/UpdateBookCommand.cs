using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Commands;
public class UpdateBookCommand : IRequest
{
    public int Id { get; set; }
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public List<int> AuthorsIds { get; set; } = default!;
    public int GenreId { get; set; }
    public int PublisherId { get; set; }
}
public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public UpdateBookCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await _entityExistenceValidator.ValidateAsync<Genre>(request.GenreId, cancellationToken);

        var publisher = await _repository.GetByIdAsync<Publisher>(request.PublisherId, cancellationToken);
        _entityExistenceValidator.ValidateAsync(publisher, request.Id);
        publisher.Approved = true;

        await _entityExistenceValidator.ValidateAsync<Author>(request.AuthorsIds, cancellationToken);
        var authors = await _repository.GetByPredicateAsync<Author>
            (a => request.AuthorsIds.Contains(a.Id), cancellationToken);
        foreach (var author in authors)
        {
            author.Approved = true;
        }

        // b => b.Authors has been removed from there. Test it
        var book = await _repository.GetByIdAsync<Book>(request.Id, cancellationToken);
        _entityExistenceValidator.ValidateAsync(book, request.Id);

        book.Isbn = request.Isbn;
        book.Title = request.Title;
        book.PublicationDate = request.PublicationDate;
        book.GenreId = request.GenreId;
        book.PublisherId = request.PublisherId;
        book.Authors = authors;

        await _repository.SaveAsync(cancellationToken);
    }
}
