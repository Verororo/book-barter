
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class BookRelationshipRepository : IBookRelationshipRepository
{
    private readonly AppDbContext _context;
    public BookRelationshipRepository(AppDbContext context)
    {
        _context = context;
    }

    public Task<bool> ExistsAsync<T>(int userId, int bookId, CancellationToken cancellationToken)
        where T : class, IBookRelationship
    {
        return _context.Set<T>().AnyAsync(x => x.UserId == userId && x.BookId == bookId, cancellationToken);
    }
}
