
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.Application.Common.Services;

public class EntityExistenceValidator : IEntityExistenceValidator
{
    private readonly IServiceProvider _serviceProvider;
    public EntityExistenceValidator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task ValidateAsync<T>(int id, CancellationToken cancellationToken)
        where T : Entity
    {
        var repository = _serviceProvider.GetRequiredService<IGenericRepository>();

        bool exists = await repository.ExistsByIdAsync<T>(id, cancellationToken);
        if (!exists) throw new EntityNotFoundException(typeof(T).Name, id);
    }
    public async Task ValidateAsync<T>(List<int> ids, CancellationToken cancellationToken)
        where T : Entity
    {
        var repository = _serviceProvider.GetRequiredService<IGenericRepository>();

        var existingIds = await repository.GetExistingIds<T>(ids, cancellationToken);
        var nonExistingIds = ids.Except(existingIds).ToList();

        if (!nonExistingIds.Any())
        {
            return;
        }

        throw new EntityNotFoundException(typeof(T).Name, nonExistingIds);
    }
}
