
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Common.Models;
using BookBarter.Infrastructure.Extensions;

namespace BookBarter.Infrastructure.Repositories;

public class BookRepository : IBookRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Book> _dbSet;
    private readonly IMapper _mapper;
    public BookRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Book>();
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
        var total = await books.CountAsync();

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

        var result = await books.ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .Sort(request)
            .Paginate(request)
            .ToListAsync(cancellationToken);

        return new PaginatedResult<BookDto>
        {
            PageNumber = request.PageNumber,
            PageSize = request.PageSize,
            Total = total,
            Items = result
        };
    }
}
