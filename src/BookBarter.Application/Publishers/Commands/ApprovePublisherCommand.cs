using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Publishers.Commands;

public class ApprovePublisherCommand : IRequest
{
    public int Id { get; set; }
}

public class ApprovePublisherCommandHandler : IRequestHandler<ApprovePublisherCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public ApprovePublisherCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task Handle(ApprovePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _repository.GetByIdAsync<Publisher>(request.Id, cancellationToken);
        await _entityExistenceValidator.ValidateAsync<Publisher>(request.Id, cancellationToken);  // FIX: use entity validator .Validate()

        if (publisher!.Approved)
        {
            return;
        }

        publisher.Approved = true;

        await _repository.SaveAsync(cancellationToken);
    }
}