using BookBarter.Application.Common.Models;
using BookBarter.Application.Publishers.Commands;
using BookBarter.Application.Publishers.Queries;
using BookBarter.Application.Publishers.Responses;
using BookBarter.Application.Users.Commands;
using BookBarter.Application.Users.Queries;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserBarter.Application.Users.Queries;

namespace BookBarter.API.Controllers;

[ApiController]
[Route("/api/[controller]")]

public class PublishersController : ControllerBase
{
    private readonly IMediator _mediator;
    public PublishersController(IMediator mediator)
    {
        _mediator = mediator;
    }
    /*
    [HttpGet]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task<UserDto> GetByIdPublisher(int id, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(new { Id = id }, cancellationToken);
        return response;
    }
    */
    [HttpPost]
    [Route("paged")]
    [AllowAnonymous]
    public async Task<PaginatedResult<PublisherDto>> GetPagedPublishers([FromBody] GetPagedPublishersQuery getPagedPublishersQuery,
        CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(getPagedPublishersQuery, cancellationToken);
        return response;
    }
    
    [HttpPost]
    [AllowAnonymous]
    public async Task<int?> CreatePublisher(CreatePublisherCommand command, CancellationToken cancellationToken)
    {
        var response = await _mediator.Send(command, cancellationToken);
        return response;
    }
    /*
    [HttpDelete]
    [Route("{id}")]
    [AllowAnonymous]
    public async Task DeletePublisher(int id, CancellationToken cancellationToken)
    {
        await _mediator.Send(new DeletePublisherCommand { Id = id }, cancellationToken);
    }
    */
}
