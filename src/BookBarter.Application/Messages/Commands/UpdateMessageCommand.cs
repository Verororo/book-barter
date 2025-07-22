
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Messages.Commands;

public class UpdateMessageCommand : IRequest
{
    public int Id { get; set; }
    public string Body { get; set; } = default!;
}
public class UpdateMessageCommandHandler : IRequestHandler<UpdateMessageCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    public UpdateMessageCommandHandler(IGenericRepository repository, IEntityExistenceValidator entityExistenceValidator)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
    }
    public async Task Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await _repository.GetByIdAsync<Message>(request.Id, cancellationToken);
        _entityExistenceValidator.ValidateAsync(message, request.Id);

        message.Body = request.Body;

        await _repository.SaveAsync(cancellationToken);
    }
}