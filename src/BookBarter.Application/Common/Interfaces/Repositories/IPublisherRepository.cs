
using BookBarter.Application.Authors.Queries;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Publishers.Queries;
using BookBarter.Application.Publishers.Responses;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IPublisherRepository
{
    public Task<PaginatedResult<PublisherDto>> GetDtoPagedAsync(GetPagedPublishersQuery request,
        CancellationToken cancellationToken);
}
