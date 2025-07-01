using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
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

    public Task<UserDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(u => u.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<PaginatedResult<UserDto>> GetDtoPagedAsync(GetPagedUsersQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<User> users = _dbSet;

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            users = users.Where(u => u.UserName!.Contains(request.UserName));
        }

        if (!string.IsNullOrWhiteSpace(request.City))
        {
            // StringComparison is set to compare strings culture-invariantly
            users = users.Where(u => u.City.Equals(request.City, StringComparison.InvariantCultureIgnoreCase));
        }

        if (request.OwnedBooksIds?.Any() == true)
        {
            users = users.Where(u => request.OwnedBooksIds
                .All(bookId => u.OwnedBooks.Any(ob => ob.BookId == bookId)));
        }

        if (request.WantedBooksIds?.Any() == true)
        {
            users = users.Where(u => request.WantedBooksIds
                .All(bookId => u.WantedBooks.Any(wb => wb.Id == bookId)));
        }

        var paginatedResult = await PaginationExtensions.CreatePaginatedResultAsync<User, UserDto>
            (users, request, _mapper, cancellationToken);

        // seeding (?)

        return paginatedResult;
    }
}
