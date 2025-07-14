
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Commands;

public class ApproveBookCommand : IRequest
{
    public int Id { get; set; }
}

public class ApproveBookCommandHandler : IRequestHandler<ApproveBookCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public ApproveBookCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task Handle(ApproveBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _repository.GetByIdAsync<Book>(request.Id, cancellationToken, b => b.Publisher, b => b.Authors);
        await _entityExistenceValidator.ValidateAsync<Book>(request.Id, cancellationToken);

        if (book!.Approved)
        {
            return;
        }

        book.Approved = true;

        if (!book.Publisher.Approved)
        {
            book.Publisher.Approved = true;
        }

        foreach (var author in book.Authors)
        {
            if (!author.Approved)
            {
                author.Approved = true;
            }
        }

        await _repository.SaveAsync(cancellationToken);
    }
}