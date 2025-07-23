
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Publishers.Commands;

public class UpdatePublisherCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
}
public class UpdatePublisherCommandHandler : IRequestHandler<UpdatePublisherCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public UpdatePublisherCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }
    public async Task Handle(UpdatePublisherCommand request, CancellationToken cancellationToken)
    {
        var publisher = await _repository.GetByIdAsync<Publisher>(request.Id, cancellationToken);
        _entityExistenceValidator.ValidateAsync(publisher, request.Id);

        publisher.Name = request.Name;

        await _repository.SaveAsync(cancellationToken);
    }
}