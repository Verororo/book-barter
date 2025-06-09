
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common.Interfaces;

public interface IEntityExistenceValidator
{
    Task ValidateAsync<T>(int id, CancellationToken cancellationToken)
        where T : Entity;
    Task ValidateAsync<T>(List<int> ids, CancellationToken cancellationToken)
        where T : Entity;
}