
using System.Linq.Expressions;
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : Entity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public Repository(AppDbContext context)
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
    public Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
    {
        IQueryable<T> query = _dbSet;
        if (includes != null)
        {
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
        }

        return query.FirstOrDefaultAsync(e => e.Id == id);
    }
    public Task<bool> ExistsByIdAsync(int id)
    {
        return _dbSet.AnyAsync(x => x.Id == id);
    }
    public Task SaveAsync()
    {
        return _context.SaveChangesAsync();
    }
}
