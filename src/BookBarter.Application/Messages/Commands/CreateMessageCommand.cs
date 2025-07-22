
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Messages.Commands;

public class CreateMessageCommand : IRequest<int>
{
    public int ReceiverId { get; set; }
    public string Body { get; set; } = default!;
}

public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, int>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    private readonly ICurrentUserProvider _currentUserProvider;
    public CreateMessageCommandHandler(IGenericRepository repository,
        IEntityExistenceValidator entityExistenceValidator,
        ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
        _currentUserProvider = currentUserProvider;
    }

    public async Task<int> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserProvider.UserId.HasValue)
        {
            throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
        }
        var senderId = _currentUserProvider.UserId.Value;

        if (senderId == request.ReceiverId)
        {
            throw new BusinessLogicException($"Sending messages to oneself is not permitted.");
        }

        await _entityExistenceValidator.ValidateAsync<User>(senderId, cancellationToken);
        await _entityExistenceValidator.ValidateAsync<User>(request.ReceiverId, cancellationToken);

        var message = new Message
        {
            SenderId = senderId,
            ReceiverId = request.ReceiverId,
            Body = request.Body,
            SentTime = DateTime.UtcNow
        };

        _repository.Add(message);

        await _repository.SaveAsync(cancellationToken);

        return message.Id;
    }
}
