using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Domain.Exceptions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Interfaces;

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
    private readonly IEntityExistenceValidator _validator;
    public UpdateBookCommandHandler(
        IGenericRepository repository,
        IEntityExistenceValidator validator
        )
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAsync<Genre>(request.GenreId, cancellationToken);

        var publisher = await _repository.GetByIdAsync<Publisher>(request.PublisherId, cancellationToken);
        if (publisher == null) throw new EntityNotFoundException(typeof(Publisher).Name, request.Id);
        publisher.Approved = true;

        await _validator.ValidateAsync<Author>(request.AuthorsIds, cancellationToken);
        var authors = await _repository.GetByPredicateAsync<Author>
            (a => request.AuthorsIds.Contains(a.Id), cancellationToken);
        foreach (var author in authors)
        {
            author.Approved = true;
        }

        var book = await _repository.GetByIdAsync<Book>(request.Id, cancellationToken, b => b.Authors);
        if (book == null) throw new EntityNotFoundException(typeof(Book).Name, request.Id);

        book.Isbn = request.Isbn;
        book.Title = request.Title;
        book.PublicationDate = request.PublicationDate;
        book.GenreId = request.GenreId;
        book.PublisherId = request.PublisherId;
        book.Authors = authors;

        await _repository.SaveAsync(cancellationToken);
    }
}
