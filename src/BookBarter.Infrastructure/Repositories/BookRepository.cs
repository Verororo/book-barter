
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces.Repositories;

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

    //public Task<Book?> GetByIdAsync(int id, CancellationToken cancellationToken)
    //{
    //    return _dbSet
    //        .Include(b => b.Authors)
    //        .FirstOrDefaultAsync(b => b.Id == id);
    //}
    public Task<BookDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(b => b.Id == id)
            .ProjectTo<BookDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync();
    }
}
