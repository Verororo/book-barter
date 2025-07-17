
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Infrastructure.Extensions;

public static class PaginationExtensions
{
    public async static Task<PaginatedResult<TDto>> CreatePaginatedResultAsync<TEntity, TDto>
        (this IQueryable<TEntity> query, PagedQuery request, IMapper mapper, CancellationToken cancellationToken)
        where TEntity : class, IEntity
        where TDto : class
    {
        var total = await query.CountAsync(cancellationToken);

        var resultQuery = query.ProjectTo<TDto>(mapper.ConfigurationProvider, new { query = request })
            .Order(request)
            .Paginate(request);

        var result = await resultQuery.ToListAsync(cancellationToken);
        /*
        if (request is GetPagedUsersQuery usersQuery && typeof(TDto) == typeof(ListedUserDto))
        {
            foreach (var item in result.OfType<ListedUserDto>())
            {
                if (usersQuery.OwnedBooksIds?.Any() == true)
                {
                    item.OwnedBooks = item.OwnedBooks
                        .OrderByDescending(ob => usersQuery.OwnedBooksIds.Contains(ob.Book.Id))
                        .ThenBy(ob => ob.AddedDate)
                        .ToList();
                }

                if (usersQuery.WantedBooksIds?.Any() == true)
                {
                    item.WantedBooks = item.WantedBooks
                        .OrderByDescending(wb => usersQuery.WantedBooksIds.Contains(wb.Book.Id))
                        .ThenBy(wb => wb.AddedDate)
                        .ToList();
                }
            }
        }
        */
        return new PaginatedResult<TDto>()
        {
            Items = result,
            Total = total,
            PageNumber = request.PageNumber,
            PageSize = request.PageSize
        };
    }

    private static IQueryable<T> Order<T>(this IQueryable<T> query, PagedQuery request)
    {
        if (!string.IsNullOrWhiteSpace(request.OrderByProperty))
        {
            query = query.OrderBy(request.OrderByProperty + " " + request.OrderDirection);
        }
        return query;
    }

    private static IQueryable<T> Paginate<T>(this IQueryable<T> query, PagedQuery request)
    {
        var entities = query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize);
        return entities;
    }
}
