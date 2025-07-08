using BookBarter.Application.Common.Models;
using BookBarter.Application.Genres.Queries;
using BookBarter.Application.Genres.Responses;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class GenresController : ControllerBase
{
    private readonly IMediator _mediator;
    public GenresController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /*
    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task<UserDto> GetByIdGenre(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new { Id = id }, cancellationToken);
        return response;
    }
    */
    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<GenreDto>> GetPagedGenres([FromBody] GetPagedGenresQuery getPagedGenresQuery,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(getPagedGenresQuery, cancellationToken);
        return response;
    }
}
