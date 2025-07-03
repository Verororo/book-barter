
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using MediatR;

namespace BookBarter.Application.Users.Commands;

public class UpdateUserCommand : IRequest
{
    public int Id { get; set; }
    public string? About { get; set; }
    public int CityId { get; set; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly IGenericRepository _repository;
    private readonly IEntityExistenceValidator _existenceValidator;
    public UpdateUserCommandHandler(IGenericRepository repository, IEntityExistenceValidator existenceValidator)
    {
        _repository = repository;
        _existenceValidator = existenceValidator;
    }
    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync<User>(request.Id, cancellationToken);
        if (user == null) { throw new EntityNotFoundException(typeof(User).Name, request.Id); }
        
        await _existenceValidator.ValidateAsync<City>(request.CityId, cancellationToken);

        user.About = request.About;
        user.CityId = request.CityId;

        await _repository.SaveAsync(cancellationToken);
    }
}