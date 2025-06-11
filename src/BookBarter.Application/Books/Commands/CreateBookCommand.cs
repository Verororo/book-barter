using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Services;
using BookBarter.Domain.Entities;
using MediatR;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using BookBarter.Application.Common.Interfaces;

namespace BookBarter.Application.Books.Commands;
public class CreateBookCommand : IRequest
{
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public List<int> AuthorsIds { get; set; } = default!;
    public int GenreId { get; set; }
    public int PublisherId { get; set; }
}

public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _validator;
    public CreateBookCommandHandler(
        IGenericRepository repository,
        IEntityExistenceValidator validator
        )
    {
        _repository = repository;
        _validator = validator;
    }
    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAsync<Genre>(request.GenreId, cancellationToken); 
        await _validator.ValidateAsync<Publisher>(request.PublisherId, cancellationToken);
        await _validator.ValidateAsync<Author>(request.AuthorsIds, cancellationToken);

        var existingAuthors = await _repository.GetByPredicateAsync<Author>
            (a => request.AuthorsIds.Contains(a.Id), cancellationToken);

        var book = new Book
        {
            Isbn = request.Isbn,
            Title = request.Title,
            PublicationDate = request.PublicationDate,
            GenreId = request.GenreId,
            PublisherId = request.PublisherId,
            Authors = existingAuthors
        };

        _repository.Add<Book>(book);
        await _repository.SaveAsync(cancellationToken);
    }
}
