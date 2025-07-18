
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
    private readonly IEntityExistenceValidator _existenceValidator;
    private readonly ICurrentUserProvider _currentUserProvider;
    public UpdateUserCommandHandler(IGenericRepository repository, 
        IEntityExistenceValidator existenceValidator, 
        ICurrentUserProvider currentUserProvider)
    {
        _repository = repository;
        _existenceValidator = existenceValidator;
        _currentUserProvider = currentUserProvider;
    }
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var id = (int)_currentUserProvider.UserId!; // COMMENT

        var user = await _repository.GetByIdAsync<User>(id, cancellationToken);
        if (user == null) { throw new EntityNotFoundException(typeof(User).Name, id); } // COMMENT

        await _existenceValidator.ValidateAsync<City>(request.CityId, cancellationToken);

        user.About = request.About;
        user.CityId = request.CityId;

        await _repository.SaveAsync(cancellationToken);
    }
}