using BookBarter.Application.Cities.Queries;
using BookBarter.Application.Cities.Responses;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
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
    /*
    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task<UserDto> GetByIdCity(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new { Id = id }, cancellationToken);
        return response;
    }
    */
    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<CityDto>> GetPagedCities([FromBody] GetPagedCitiesQuery getPagedCitiesQuery,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(getPagedCitiesQuery, cancellationToken);
        return response;
    }
}
