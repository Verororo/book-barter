
using AutoMapper;
using BookBarter.Application.Cities.Queries;
using BookBarter.Application.Cities.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<City> _dbSet;
    private readonly IMapper _mapper;
    public CityRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<City>();
        _mapper = mapper;
    }

    public async Task<PaginatedResult<CityDto>> GetDtoPagedAsync(GetPagedCitiesQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<City> cities = _dbSet;

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            cities = cities.Where(c => c.Name.Contains(request.Query) || c.NameAscii.Contains(request.Query));
        }
        
        var paginatedResult = await cities.CreatePaginatedResultAsync<City, CityDto>(request, _mapper, cancellationToken);

        return paginatedResult;
    }
}
