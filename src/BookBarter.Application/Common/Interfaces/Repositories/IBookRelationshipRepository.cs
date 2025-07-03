using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IBookRelationshipRepository 
{
    Task<bool> ExistsAsync<T>(int userId, int bookId, CancellationToken cancellationToken)
        where T : class, IBookRelationship;
}
