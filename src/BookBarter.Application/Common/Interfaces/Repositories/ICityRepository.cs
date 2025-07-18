using BookBarter.Application.Cities.Queries;
using BookBarter.Application.Cities.Responses;
using BookBarter.Application.Common.Models;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface ICityRepository
{
    public Task<PaginatedResult<CityDto>> GetDtoPagedAsync(GetPagedCitiesQuery request,
        CancellationToken cancellationToken);
}
