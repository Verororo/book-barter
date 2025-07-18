
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
        _entityExistenceValidator.ValidateAsync(author, request.Id);

        if (author.Approved)
        {
            return;
        }

        author.Approved = true;

        await _repository.SaveAsync(cancellationToken);
    }
}