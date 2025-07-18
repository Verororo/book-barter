
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Models;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IBookRepository
{
    Task<BookDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken);
    Task<PaginatedResult<BookDto>> GetDtoPagedAsync(GetPagedBooksQuery query, CancellationToken cancellationToken);
    Task<PaginatedResult<BookForModerationDto>> GetDtoForModerationPagedAsync(GetPagedBooksForModerationQuery query, CancellationToken cancellationToken);

}
