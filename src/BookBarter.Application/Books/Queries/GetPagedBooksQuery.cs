using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using MediatR;

namespace BookBarter.Application.Books.Queries;

public class GetPagedBooksQuery : PagedQuery, IRequest<PaginatedResult<BookDto>>
{
    public bool SkipLoggedInUserBooks { get; set; }
    public string? Title { get; set; }
    public int? AuthorId { get; set; }
    public int? GenreId { get; set; }
    public int? PublisherId { get; set; }
}

public class GetPagedBooksQueryHandler : IRequestHandler<GetPagedBooksQuery, PaginatedResult<BookDto>>
{
    private readonly IBookRepository _bookRepository;

    public GetPagedBooksQueryHandler(IBookRepository bookRepository)
    {
        _bookRepository = bookRepository;
    }

    public async Task<PaginatedResult<BookDto>> Handle(GetPagedBooksQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await _bookRepository.GetDtoPagedAsync(request, cancellationToken);

        return result;
    }
}
