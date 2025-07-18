using BookBarter.Domain.Entities.Abstractions;

using System.Diagnostics.CodeAnalysis;

namespace BookBarter.Application.Common.Interfaces;

public interface IEntityExistenceValidator
{
    Task ValidateAsync<T>(int id, CancellationToken cancellationToken)
        where T : class, IEntity;
    Task ValidateAsync<T>(List<int> ids, CancellationToken cancellationToken)
        where T : class, IEntity;

    // FIX: Add
    //void Validate<T>([NotNull] T? entity, int id)
    //where T : class, IEntity;
}