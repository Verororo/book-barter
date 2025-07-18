using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using MediatR;

namespace BookBarter.Application.Genres.Queries;

public class GetPagedGenresQuery : PagedQuery, IRequest<PaginatedResult<GenreDto>>
{
    public string? Query { get; set; }
}

public class GetPagedGenresQueryHandler : IRequestHandler<GetPagedGenresQuery, PaginatedResult<GenreDto>>
{
    private readonly IGenreRepository _genreRepository;
    public GetPagedGenresQueryHandler(IGenreRepository genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public Task<PaginatedResult<GenreDto>> Handle(GetPagedGenresQuery request, CancellationToken cancellationToken)
    {
        return _genreRepository.GetDtoPagedAsync(request, cancellationToken);
    }
}