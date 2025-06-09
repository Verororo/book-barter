using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common.Interfaces.Repositories;

// This repository is used for read operations without any further modifications of the objects.
// Its getters implementations use .AsNoTracking() for EF Core to not track the objects' state.
public interface IReadingRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id, CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken, 
        params Expression<Func<T, object>>[] includes);
    Task<List<T>> GetByPredicateAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken,
        params Expression<Func<T, object>>[] includes);
    Task<bool> ExistsByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<int>> GetExistingIds(List<int> ids, CancellationToken cancellationToken);
}
