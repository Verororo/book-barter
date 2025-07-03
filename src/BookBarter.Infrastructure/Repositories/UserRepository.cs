using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<User> _dbSet;
    private readonly IMapper _mapper;
    public UserRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<User>();
        _mapper = mapper;
    }

    public Task<TDto?> GetDtoByIdAsync<TDto>(int id, CancellationToken cancellationToken)
        where TDto : class
    {
        return _dbSet
            .Where(u => u.Id == id)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginatedResult<TDto>> GetDtoPagedAsync<TDto>(GetPagedUsersQuery request,
        CancellationToken cancellationToken)
        where TDto : class
    {
        IQueryable<User> users = _dbSet;

        // MAIN FILTERS

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            users = users.Where(u => u.UserName!.Contains(request.UserName));
        }

        if (request.CityId.HasValue)
        {
            // StringComparison is set to compare strings culture-invariantly
            users = users.Where(u => u.CityId.Equals(request.CityId));
        }

        // OWNED BOOKS FILTERS

        if (request.OwnedBooksIds?.Any() == true)
        {
            users = users.Where(u => request.OwnedBooksIds
                .All(bookId => u.OwnedBooks.Any(ob => ob.BookId == bookId)));
        }

        if (request.OwnedBookAuthorsIds?.Any() == true)
        {
            users = users.Where(u => request.OwnedBookAuthorsIds
                .All(authorId => u.OwnedBooks.Any(ob => ob.Book.Authors.Any(a => a.Id == authorId))));
        }

        if (request.OwnedBookGenreId.HasValue)
        {
            users = users.Where(u => u.OwnedBooks.Any(ob => ob.Book.GenreId == request.OwnedBookGenreId));
        }

        if (request.OwnedBookPublisherId.HasValue)
        {
            users = users.Where(b => b.OwnedBooks.Any(ob => ob.Book.PublisherId == request.OwnedBookPublisherId));
        }

        // WANTED BOOKS FILTERS

        if (request.WantedBooksIds?.Any() == true)
        {
            users = users.Where(u => request.WantedBooksIds
                .All(bookId => u.WantedBooks.Any(ob => ob.BookId == bookId)));
        }

        if (request.WantedBookAuthorsIds?.Any() == true)
        {
            users = users.Where(u => request.WantedBookAuthorsIds
                .All(authorId => u.WantedBooks.Any(ob => ob.Book.Authors.Any(a => a.Id == authorId))));
        }

        if (request.WantedBookGenreId.HasValue)
        {
            users = users.Where(u => u.WantedBooks.Any(ob => ob.Book.GenreId == request.WantedBookGenreId));
        }

        if (request.WantedBookPublisherId.HasValue)
        {
            users = users.Where(b => b.WantedBooks.Any(ob => ob.Book.PublisherId == request.WantedBookPublisherId));
        }

        var paginatedResult = await PaginationExtensions.CreatePaginatedResultAsync<User, TDto>
            (users, request, _mapper, cancellationToken);

        return paginatedResult;
    }
}
