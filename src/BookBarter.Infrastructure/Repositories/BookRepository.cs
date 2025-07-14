using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Common.Models;
using BookBarter.Infrastructure.Extensions;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Interfaces;
using System.Linq;

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

    public async Task<PaginatedResult<BookDto>> GetDtoPagedAsync(GetPagedBooksQuery request, 
        CancellationToken cancellationToken)
    {
        IQueryable<Book> books = _dbSet;

        if (request.SkipLoggedInUserBooks)
        {
            var userId = _currentUserProvider.UserId;
            if (userId == null) { throw new Exception("SkipLoggedInUserBooks was specified, but program failed to get UserId"); }

            books = books.Where(b => !b.WantedByUsers.Any(wb => wb.UserId == userId));
            books = books.Where(b => !b.OwnedByUsers.Any(wb => wb.UserId == userId));
        }

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            books = books.Where(b => b.Title.Contains(request.Title));
        }

        if (!string.IsNullOrWhiteSpace(request.AuthorName))
        {
            // split the query into multiple strings
            var tokens = request.AuthorName
                .Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(t => t.Trim())
                .Where(t => !string.IsNullOrEmpty(t))
                .ToList();

            var loweredTokens = tokens.Select(t => t.ToLower()).ToList();

            foreach (var token in loweredTokens)
            {
                books = books.Where(b =>
                    b.Authors.Any(a =>
                        // concatenate first+space+last, ToLower() for case-insensitive
                        (a.FirstName + " " + a.LastName).ToLower().Contains(token)
                    )
                );
            }
        }

        if (request.GenreId.HasValue)
        {
            books = books.Where(b => b.GenreId == request.GenreId);
        }

        if (request.PublisherId.HasValue)
        {
            books = books.Where(b => b.PublisherId == request.PublisherId);
        }

        var paginatedResult = await books.CreatePaginatedResultAsync<Book, BookDto>(request, _mapper, cancellationToken);

        return paginatedResult;
    }

    public async Task<PaginatedResult<BookForModerationDto>> GetDtoForModerationPagedAsync(GetPagedBooksForModerationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Book> books = _dbSet;

        books = books.Where(b => b.Approved == request.Approved);

        var paginatedResult = await books.CreatePaginatedResultAsync<Book, BookForModerationDto>(request, _mapper, cancellationToken);

        return paginatedResult;
    }
}
