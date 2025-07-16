using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Authors.Queries;

public class GetPagedAuthorsQuery : PagedQuery, IRequest<PaginatedResult<AuthorDto>>
{
    public string? Query { get; set; } = default!;
    public List<int>? IdsToSkip { get; set; }
}

public class GetPagedAuthorsQueryHandler : IRequestHandler<GetPagedAuthorsQuery, PaginatedResult<AuthorDto>>
{
    private readonly IAuthorRepository _authorRepository;
    public GetPagedAuthorsQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public async Task<PaginatedResult<AuthorDto>> Handle(GetPagedAuthorsQuery request, CancellationToken cancellationToken)
    {
        var result = await _authorRepository.GetDtoPagedAsync(request, cancellationToken);

        return result;
    }
}