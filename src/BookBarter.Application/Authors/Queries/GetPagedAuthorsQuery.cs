using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
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

    public Task<PaginatedResult<AuthorDto>> Handle(GetPagedAuthorsQuery request, CancellationToken cancellationToken)
    {
        return _authorRepository.GetDtoPagedAsync(request, cancellationToken);
    }
}