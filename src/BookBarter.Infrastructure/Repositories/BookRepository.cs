using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Book> _dbSet;
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IMapper _mapper;
    public BookRepository(AppDbContext context, IMapper mapper, ICurrentUserProvider currentUserProvider)
    {
        _context = context;
        _dbSet = _context.Set<Book>();
        _currentUserProvider = currentUserProvider;
        _mapper = mapper;
    }

    public Task<BookDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(b => b.Id == id)
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PaginatedResult<BookDto>> GetDtoPagedAsync(GetPagedBooksQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Book> books = _dbSet;

        if (request.SkipLoggedInUserBooks)
        {
            if (!_currentUserProvider.UserId.HasValue)
            {
                throw new BusinessLogicException($"Failed to get user id from the current user provider. The method may have been called without authentication.");
            }

            var userId = _currentUserProvider.UserId.Value;

            books = books.Where(b => !b.WantedByUsers.Any(wb => wb.UserId == userId)
                                  && !b.OwnedByUsers.Any(wb => wb.UserId == userId));
        }

        if (request.IdsToSkip != null && request.IdsToSkip.Count != 0)
        {
            books = books.Where(b => !request.IdsToSkip.Contains(b.Id));
        }

        books = books.Where(b => b.Approved == true);

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            books = books.Where(b => b.Title.Contains(request.Title));
        }
        
        if (request.AuthorId.HasValue)
        {
            books = books.Where(b => b.Authors.Any(a => a.Id == request.AuthorId));
        }

        if (request.GenreId.HasValue)
        {
            books = books.Where(b => b.GenreId == request.GenreId);
        }

        if (request.PublisherId.HasValue)
        {
            books = books.Where(b => b.PublisherId == request.PublisherId);
        }

        return books.CreatePaginatedResultAsync<Book, BookDto>(request, _mapper, cancellationToken);
    }

    public Task<PaginatedResult<BookForModerationDto>> GetDtoForModerationPagedAsync(GetPagedBooksForModerationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Book> books = _dbSet;

        books = books.Where(b => b.Approved == request.Approved);

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            books = books.Where(b => b.Title.Contains(request.Title));
        }

        if (request.AuthorId.HasValue)
        {
            books = books.Where(b => b.Authors.Any(a => a.Id == request.AuthorId));
        }

        if (request.GenreId.HasValue)
        {
            books = books.Where(b => b.GenreId == request.GenreId);
        }

        if (request.PublisherId.HasValue)
        {
            books = books.Where(b => b.PublisherId == request.PublisherId);
        }

        return books.CreatePaginatedResultAsync<Book, BookForModerationDto>(request, _mapper, cancellationToken);
    }
}
