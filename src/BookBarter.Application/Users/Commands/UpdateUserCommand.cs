
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public string? About { get; set; }
    public int CityId { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _entityExistenceValidator;
    private readonly ICurrentUserProvider _currentUserProvider;
    public UpdateUserCommandHandler(IGenericRepository repository, 
        IEntityExistenceValidator entityExistenceValidator, 
        ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _entityExistenceValidator = entityExistenceValidator;
        _currentUserProvider = currentUserProvider;
    }
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUserProvider.UserId.HasValue)
        {
            throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
        }

        var userId = _currentUserProvider.UserId.Value;

        var user = await _repository.GetByIdAsync<User>(userId, cancellationToken);
        _entityExistenceValidator.ValidateAsync(user, userId);

        await _entityExistenceValidator.ValidateAsync<City>(request.CityId, cancellationToken);

        user.About = request.About;
        user.CityId = request.CityId;

        await _repository.SaveAsync(cancellationToken);
    }
}