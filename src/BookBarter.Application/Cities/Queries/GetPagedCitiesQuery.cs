using BookBarter.Application.Cities.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Cities.Queries;

public class GetPagedCitiesQuery : PagedQuery, IRequest<PaginatedResult<CityDto>>
{
    public string? Query { get; set; }
}

public class GetPagedCitiesQueryHandler : IRequestHandler<GetPagedCitiesQuery, PaginatedResult<CityDto>>
{
    private readonly ICityRepository _cityRepository;
    public GetPagedCitiesQueryHandler(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<PaginatedResult<CityDto>> Handle(GetPagedCitiesQuery request, CancellationToken cancellationToken)
    {
        var result = await _cityRepository.GetDtoPagedAsync(request, cancellationToken);

        return result;
    }
}