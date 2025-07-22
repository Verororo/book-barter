
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Messages.Commands;

public class DeleteMessageCommand : IRequest
{
    public int Id { get; set; }
}

public class DeleteMessageCommandHandler : IRequestHandler<DeleteMessageCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public DeleteMessageCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }

    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _repository.GetByIdAsync<Message>(request.Id, cancellationToken);
        _entityExistenceValidator.ValidateAsync(message, request.Id);

        _repository.Delete(message);

        await _repository.SaveAsync(cancellationToken);
    }
}