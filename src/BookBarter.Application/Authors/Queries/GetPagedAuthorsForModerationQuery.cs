using BookBarter.Application.Authors.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using MediatR;

namespace BookBarter.Application.Authors.Queries;

public class GetPagedAuthorsForModerationQuery : PagedQuery, IRequest<PaginatedResult<AuthorForModerationDto>>
{
    public bool Approved { get; set; }
    public string? Query { get; set; } = default!;
}

public class GetPagedAuthorsForModerationQueryHandler : IRequestHandler<GetPagedAuthorsForModerationQuery, PaginatedResult<AuthorForModerationDto>>
{
    private readonly IAuthorRepository _authorRepository;
    public GetPagedAuthorsForModerationQueryHandler(IAuthorRepository authorRepository)
    {
        _authorRepository = authorRepository;
    }

    public Task<PaginatedResult<AuthorForModerationDto>> Handle(GetPagedAuthorsForModerationQuery request, CancellationToken cancellationToken)
    {
        return _authorRepository.GetDtoForModerationPagedAsync(request, cancellationToken);
    }
}
