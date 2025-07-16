
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Publishers.Responses;
using MediatR;

namespace BookBarter.Application.Publishers.Queries;

public class GetPagedPublishersForModerationQuery : PagedQuery, IRequest<PaginatedResult<PublisherForModerationDto>>
{
    public bool Approved { get; set; }
    public string? Query { get; set; } = default!;
}

public class GetPagedPublishersForModerationQueryHandler : IRequestHandler<GetPagedPublishersForModerationQuery, PaginatedResult<PublisherForModerationDto>>
{
    private readonly IPublisherRepository _publisherRepository;
    public GetPagedPublishersForModerationQueryHandler(IPublisherRepository publisherRepository)
    {
        _publisherRepository = publisherRepository;
    }

    public async Task<PaginatedResult<PublisherForModerationDto>> Handle(GetPagedPublishersForModerationQuery request, CancellationToken cancellationToken)
    {
        var result = await _publisherRepository.GetDtoForModerationPagedAsync(request, cancellationToken);

        return result;
    }
}