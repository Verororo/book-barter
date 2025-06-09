
using BookBarter.Application.Books.Responses;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IBookRepository
{
    Task<BookDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken);
}
