
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Models;
using BookBarter.Domain.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace BookBarter.Infrastructure.Extensions;

public static class PaginationExtensions
{
    public async static Task<PaginatedResult<TDto>> CreatePaginatedResultAsync<TEntity, TDto>
        (this IQueryable<TEntity> query, PagedQuery request, IMapper mapper, CancellationToken cancellationToken)
        where TEntity : class, IEntity
        where TDto : class
    {
        var total = await query.CountAsync(cancellationToken);

        var resultQuery = query.ProjectTo<TDto>(mapper.ConfigurationProvider)
            .Order(request)
            .Paginate(request);

        var result = await resultQuery.ToListAsync(cancellationToken);
        
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
