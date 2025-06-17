
using BookBarter.Application.Books.Queries;
using System.Linq.Dynamic.Core;

namespace BookBarter.Infrastructure.Extensions;

public static class PaginationExtensions
{
    public static IQueryable<T> Sort<T>(this IQueryable<T> query, GetPagedBooksQuery request)
    {
        if (!string.IsNullOrWhiteSpace(request.OrderByProperty))
        {
            query = query.OrderBy(request.OrderByProperty + " " + request.OrderDirection);
        }
        return query;
    }

    public static IQueryable<T> Paginate<T>(this IQueryable<T> query, GetPagedBooksQuery request)
    {
        var entities = query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        return entities;
    }
}
