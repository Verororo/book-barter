
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
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

    public Task<PaginatedResult<PublisherDto>> Handle(GetPagedPublishersQuery request, CancellationToken cancellationToken)
    {
        return _publisherRepository.GetDtoPagedAsync(request, cancellationToken);
    }
}