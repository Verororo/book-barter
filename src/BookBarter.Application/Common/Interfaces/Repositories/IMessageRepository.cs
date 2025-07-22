
using BookBarter.Application.Common.Models;
using BookBarter.Application.Messages.Queries;
using BookBarter.Application.Messages.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IMessageRepository
{
    Task<MessageDto?> GetMessageByIdAsync(int id, CancellationToken cancellationToken);
    Task<PaginatedResult<MessageDto>> GetDtoPagedAsync(int userId, GetPagedMessagesQuery request, CancellationToken cancellationToken);
}
