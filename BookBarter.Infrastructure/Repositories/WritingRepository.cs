
using System.Linq.Expressions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class WritingRepository<T> : IWritingRepository<T> where T : Entity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public WritingRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }
    public void Add(T entity)
    {
        _dbSet.Add(entity);
    }
    public void Update(T entity)
    {
        _context.Update(entity);
    }
    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }
    public void Delete(List<T> entities)
    {
        _dbSet.RemoveRange(entities);
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

        return query.SingleOrDefaultAsync(e => e.Id == id);
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
            .ToListAsync();
    }
    public Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet.AnyAsync(x => x.Id == id);
    }

    public Task SaveAsync(CancellationToken cancellationToken)
    {
        return _context.SaveChangesAsync();
    }
}
