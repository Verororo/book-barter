
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Publishers.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Publishers.Queries;

public class GetPagedPublishersQuery : PagedQuery, IRequest<PaginatedResult<PublisherDto>>
{
    public string? Query { get; set; }
}

public class GetPagedPublishersQueryHandler : IRequestHandler<GetPagedPublishersQuery, PaginatedResult<PublisherDto>>
{
    private readonly IPublisherRepository _publisherRepository;
    public GetPagedPublishersQueryHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<PaginatedResult<PublisherDto>> Handle(GetPagedPublishersQuery request, CancellationToken cancellationToken)
    {
        var result = await _publisherRepository.GetDtoPagedAsync(request, cancellationToken);

        return result;
    }
}