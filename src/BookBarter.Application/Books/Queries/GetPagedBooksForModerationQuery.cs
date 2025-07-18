
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public class GetPagedBooksForModerationQuery : PagedQuery, IRequest<PaginatedResult<BookForModerationDto>>
{
    public bool Approved { get; set; }
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
    public int? GenreId { get; set; }
    public int? PublisherId { get; set; }
}

public class GetPagedBooksQueryForModerationHandler : IRequestHandler<GetPagedBooksForModerationQuery, PaginatedResult<BookForModerationDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetPagedBooksQueryForModerationHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public Task<PaginatedResult<BookForModerationDto>> Handle(GetPagedBooksForModerationQuery request,
        CancellationToken cancellationToken)
    {
        return _bookRepository.GetDtoForModerationPagedAsync(request, cancellationToken);
    }
}
