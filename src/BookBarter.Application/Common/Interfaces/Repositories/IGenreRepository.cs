using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Genres.Queries;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IGenreRepository
{
    public Task<PaginatedResult<GenreDto>> GetDtoPagedAsync(GetPagedGenresQuery request,
        CancellationToken cancellationToken);
}
