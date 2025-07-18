
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities.Abstractions;
using BookBarter.Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace BookBarter.Application.Common.Services;

public class EntityExistenceValidator : IEntityExistenceValidator
{
    private readonly IGenericRepository _repository;
    public EntityExistenceValidator(IGenericRepository repository)
    {
        _repository = repository;
    }

    public async Task ValidateAsync<T>(int id, CancellationToken cancellationToken)
        where T : class, IEntity
    {
        bool exists = await _repository.ExistsByIdAsync<T>(id, cancellationToken);
        if (!exists) throw new EntityNotFoundException(typeof(T).Name, id);
    }
    public async Task ValidateAsync<T>(List<int> ids, CancellationToken cancellationToken)
        where T : class, IEntity
    {
        var existingIds = await _repository.GetExistingIds<T>(ids, cancellationToken);
        var nonExistingIds = ids.Except(existingIds).ToList();

        if (nonExistingIds.Count == 0)
        {
            return;
        }

        throw new EntityNotFoundException(typeof(T).Name, nonExistingIds);
    }
    public void ValidateAsync<T>([NotNull] T? entity, int id)
        where T : class
    {
        if (entity == null) throw new EntityNotFoundException(typeof(T).Name, id);
    }
}
