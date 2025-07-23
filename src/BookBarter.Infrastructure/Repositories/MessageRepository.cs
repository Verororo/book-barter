using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Messages.Queries;
using BookBarter.Application.Messages.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Message> _dbSet;
    private readonly IMapper _mapper;

    public MessageRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = context.Set<Message>();
        _mapper = mapper;
    }

    public Task<MessageDto?> GetMessageByIdAsync(int id, CancellationToken cancellationToken)
    {
        return _dbSet
            .Where(m => m.Id == id)
            .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<PaginatedResult<MessageDto>> GetDtoPagedAsync(int userId, GetPagedMessagesQuery request, CancellationToken cancellationToken)
    {
        IQueryable<Message> messages = _dbSet;

        messages = messages.Where(m => m.SenderId == userId && m.ReceiverId == request.CollocutorId ||
                                       m.SenderId == request.CollocutorId && m.ReceiverId == userId);

        return messages.CreatePaginatedResultAsync<Message, MessageDto>(request, _mapper, cancellationToken);
    }
}