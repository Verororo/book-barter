using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Abstractions;

// This repository is used for read operations without any further modifications of the objects.
// Its getters implementations use .AsNoTracking() for EF Core to not track the objects' state.
public interface IReadingRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate,
        params Expression<Func<T, object>>[] includes);
    Task<bool> ExistsByIdAsync(int id);
}
