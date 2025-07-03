using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Application.Common.Interfaces;

public interface IEntityExistenceValidator
{
    Task ValidateAsync<T>(int id, CancellationToken cancellationToken)
        where T : class, IEntity;
    Task ValidateAsync<T>(List<int> ids, CancellationToken cancellationToken)
        where T : class, IEntity;
}