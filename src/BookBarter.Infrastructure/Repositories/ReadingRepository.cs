
using System.Linq.Expressions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class ReadingRepository<T> : IReadingRepository<T> where T : Entity
{
    // merge Writing and Reading repos into one
    // and use generic methods in it
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public ReadingRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public Task<List<T>> GetAllAsync(CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query
            .AsNoTracking()
            .ToListAsync();
    }
    public Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, 
        CancellationToken cancellationToken, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }
    public Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(x => x.Id == id);
    }

    public Task<List<int>> GetExistingIds(List<int> ids, CancellationToken cancellationToken)
    {
        if (ids == null || !ids.Any())
            return Task.FromResult(new List<int>());

        return _dbSet
            .Where(e => ids.Contains(e.Id))
            .Select(e => e.Id)
            .ToListAsync();
    }
}
