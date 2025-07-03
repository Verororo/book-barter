
using System.Linq.Expressions;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common.Interfaces.Repositories;
public interface IGenericRepository
{
    void Add<T>(T entity) where T : class, IEntity;
    void Update<T>(T entity) where T : class, IEntity;
    void Delete<T>(T entity) where T : class, IEntity;
    void Delete<T>(List<T> entities) where T : class, IEntity;
    Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes) where T : class, IEntity;
    Task<List<T>> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes) where T : class, IEntity;
    Task<bool> ExistsByIdAsync<T>(int id, CancellationToken cancellationToken) where T : class, IEntity;
    public Task<List<int>> GetExistingIds<T>(List<int> ids, CancellationToken cancellationToken) 
        where T : class, IEntity;
    Task SaveAsync(CancellationToken cancellationToken);
}
