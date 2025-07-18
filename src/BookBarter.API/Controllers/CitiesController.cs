using BookBarter.Application.Cities.Queries;
using BookBarter.Application.Cities.Responses;
using BookBarter.Application.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class CitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    public CitiesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public Task<PaginatedResult<CityDto>> GetPagedCities([FromBody] GetPagedCitiesQuery getPagedCitiesQuery,
        CancellationToken cancellationToken)
    {
        return _mediator.Send(getPagedCitiesQuery, cancellationToken);
    }
}
