
using System.Linq.Expressions;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Application.Abstractions;

// rename to IWritingRepository
public interface IRepository<T> where T : Entity
{
    void Add(T entity);
    void Update(T entity);
    void Delete(T entity);
    void Delete(List<T> entities);
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes); // remove?
    Task<bool> ExistsByIdAsync(int id);
    Task SaveAsync();
}
