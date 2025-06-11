
using System.Linq.Expressions;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Application.Common.Interfaces.Repositories;
public interface IGenericRepository
{
    void Add<T>(T entity) where T : Entity;
    void Update<T>(T entity) where T : Entity;
    void Delete<T>(T entity) where T : Entity;
    void Delete<T>(List<T> entities) where T : Entity;
    Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes) where T : Entity;
    Task<List<T>> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes) where T : Entity;
    Task<bool> ExistsByIdAsync<T>(int id, CancellationToken cancellationToken) where T : Entity;
    public Task<List<int>> GetExistingIds<T>(List<int> ids, CancellationToken cancellationToken) where T : Entity;
    Task SaveAsync(CancellationToken cancellationToken);
}
