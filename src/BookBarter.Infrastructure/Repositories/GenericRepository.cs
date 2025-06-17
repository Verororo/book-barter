
using System.Linq.Expressions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class GenericRepository : IGenericRepository
{
    private readonly AppDbContext _context;
    public GenericRepository(AppDbContext context)
    {
        _context = context;
    }
    public void Add<T>(T entity) where T : Entity
    {
        _context.Set<T>().Add(entity);
    }
    public void Update<T>(T entity) where T : Entity
    {
        _context.Set<T>().Update(entity);
    }
    public void Delete<T>(T entity) where T : Entity
    {
        _context.Set<T>().Remove(entity);
    }
    public void Delete<T>(List<T> entities) where T : Entity
    {
        _context.Set<T>().RemoveRange(entities);
    }
    public Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes) where T : Entity
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.SingleOrDefaultAsync(e => e.Id == id);
    }
    public Task<List<T>> GetByPredicateAsync<T>(Expression<Func<T, bool>> predicate, 
        CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes) where T : Entity
    {
        IQueryable<T> query = _context.Set<T>();
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query
            .Where(predicate)
            .ToListAsync();
    }
    public Task<bool> ExistsByIdAsync<T>(int id, CancellationToken cancellationToken) where T : Entity
    {
        return _context.Set<T>().AnyAsync(x => x.Id == id, cancellationToken: cancellationToken);
    }
    public Task<List<int>> GetExistingIds<T>(List<int> ids, CancellationToken cancellationToken) 
        where T : Entity
    {
        if (ids == null || !ids.Any())
            return Task.FromResult(new List<int>());

        return _context.Set<T>()
            .Where(e => ids.Contains(e.Id))
            .Select(e => e.Id)
            .ToListAsync(cancellationToken: cancellationToken);
    }
    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
