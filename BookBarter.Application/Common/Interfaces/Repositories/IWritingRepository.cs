
using System.Linq.Expressions;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Application.Common.Interfaces.Repositories;
public interface IWritingRepository<T> where T : Entity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(List<T> entities);
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
    Task SaveAsync(CancellationToken cancellationToken);
}
