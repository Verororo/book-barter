using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Users.Responses;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task<UserDto?> GetDtoByIdAsync(int id, CancellationToken cancellationToken);
    Task<PaginatedResult<UserDto>> GetDtoPagedAsync(GetPagedUsersQuery query, CancellationToken cancellationToken);

}
