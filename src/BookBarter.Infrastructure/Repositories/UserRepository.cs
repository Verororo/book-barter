using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces;
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
    private readonly ICurrentUserProvider _currentUserProvider;
    private readonly IMapper _mapper;
    public UserRepository(AppDbContext context, IMapper mapper, ICurrentUserProvider currentUserProvider)
    {
        _context = context;
        _dbSet = _context.Set<User>();
        _currentUserProvider = currentUserProvider;
        _mapper = mapper;
    }

    public Task<TDto?> GetDtoByIdAsync<TDto>(int id, bool excludeUnapprovedBooks, CancellationToken cancellationToken)
        where TDto : class
    {
        var parameters = new { excludeUnapprovedBooks };

        return _dbSet
            .Where(u => u.Id == id)
            .ProjectTo<TDto>(_mapper.ConfigurationProvider, parameters)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PaginatedResult<TDto>> GetDtoPagedAsync<TDto>(GetPagedUsersQuery request,
        CancellationToken cancellationToken)
        where TDto : class
    {
        IQueryable<User> users = _dbSet;

        // Don't return users with no lists specified
        users = users.Where(u => u.WantedBooks.Any(wb => wb.Book.Approved)
                              && u.OwnedBooks.Any(ob => ob.Book.Approved));

        // MAIN FILTERS
        if (_currentUserProvider.UserId.HasValue)
        {
            users = users.Where(u => u.Id != _currentUserProvider.UserId);
        }

        if (!string.IsNullOrWhiteSpace(request.UserName))
        {
            users = users.Where(u => u.UserName!.Contains(request.UserName));
        }

        if (request.CityId.HasValue)
        {
            users = users.Where(u => u.CityId.Equals(request.CityId));
        }

        // OWNED BOOKS FILTERS

        if (request.OwnedBooksIds != null && request.OwnedBooksIds.Count != 0)
        {
            users = users.Where(u => request.OwnedBooksIds
                .All(bookId => u.OwnedBooks.Any(ob => ob.BookId == bookId)));
        }

        if (request.OwnedBookAuthorsIds != null && request.OwnedBookAuthorsIds.Count != 0)
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

        if (request.WantedBooksIds != null && request.WantedBooksIds.Count != 0)
        {
            users = users.Where(u => request.WantedBooksIds
                .All(bookId => u.WantedBooks.Any(ob => ob.BookId == bookId)));
        }

        if (request.WantedBookAuthorsIds != null && request.WantedBookAuthorsIds.Count != 0)
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

        return users.CreatePaginatedResultAsync<User, TDto>(request, _mapper, cancellationToken);
    }

    public async Task<List<MessagingUserDto>> GetUserChatsAsync(int userId, CancellationToken cancellationToken)
    {
        var sentMessagesQuery = _dbSet
            .Where(u => u.Id == userId)
            .SelectMany(u => u.SentMessages)
            .Select(m => new
            {
                ChatUserId = m.ReceiverId,
                ChatUserName = m.Receiver.UserName,
                Message = m
            });

        var receivedMessagesQuery = _dbSet
            .Where(u => u.Id == userId)
            .SelectMany(u => u.ReceivedMessages)
            .Select(m => new
            {
                ChatUserId = m.SenderId,
                ChatUserName = m.Sender.UserName,
                Message = m
            });

        var allMessagesQuery = sentMessagesQuery.Union(receivedMessagesQuery);

        var allChats = await allMessagesQuery
            .GroupBy(x => new { x.ChatUserId, x.ChatUserName })
            .Select(g => new MessagingUserDto
            {
                Id = g.Key.ChatUserId,
                UserName = g.Key.ChatUserName!,
                LastMessage = g
                    .OrderByDescending(x => x.Message.SentTime)
                    .First().Message.Body,
                LastMessageTime = g
                    .OrderByDescending(x => x.Message.SentTime)
                    .First().Message.SentTime
            })
            .OrderByDescending(x => x.LastMessageTime)
            .ToListAsync(cancellationToken);

        return allChats;
    }
}
