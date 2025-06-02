using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class ReadingRepository<T> : IReadingRepository<T> where T : Entity
{
    private readonly AppDbContext _context;
    private readonly DbSet<T> _dbSet;
    public ReadingRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
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

        return query
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Id == id);
    }
    public Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
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
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }
}
