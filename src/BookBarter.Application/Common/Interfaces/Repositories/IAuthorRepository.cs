
using BookBarter.Application.Authors.Queries;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IAuthorRepository
{
    public Task<PaginatedResult<AuthorDto>> GetDtoPagedAsync(GetPagedAuthorsQuery request,
        CancellationToken cancellationToken);
}
