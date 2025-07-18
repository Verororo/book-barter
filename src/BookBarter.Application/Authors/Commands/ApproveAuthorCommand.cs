
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Authors.Commands;

public class ApproveAuthorCommand : IRequest
{
    public int Id { get; set; }
}

public class ApproveAuthorCommandHandler : IRequestHandler<ApproveAuthorCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public ApproveAuthorCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task Handle(ApproveAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _repository.GetByIdAsync<Author>(request.Id, cancellationToken);
        await _entityExistenceValidator.ValidateAsync<Author>(request.Id, cancellationToken);  // FIX: add method what if check if author is null.

        //_entityExistenceValidator.Validate(author, request.Id);

        // FIX: Check if author is null before accessing properties

        if (author.Approved)
        {
            return;
        }

        author.Approved = true;

        await _repository.SaveAsync(cancellationToken);
    }
}