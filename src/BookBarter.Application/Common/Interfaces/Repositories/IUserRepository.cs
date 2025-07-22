
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<TDto?> GetDtoByIdAsync<TDto>(int id, bool excludeUnapprovedBooks, CancellationToken cancellationToken)
        where TDto : class;
    Task<PaginatedResult<TDto>> GetDtoPagedAsync<TDto>(GetPagedUsersQuery query, 
        CancellationToken cancellationToken)
        where TDto : class;
    Task<List<MessagingUserDto>> GetUserChatsAsync(int userId, CancellationToken cancellationToken);
}
