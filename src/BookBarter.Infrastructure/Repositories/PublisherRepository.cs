using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Publishers.Queries;
using BookBarter.Application.Publishers.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class PublisherRepository : IPublisherRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Publisher> _dbSet;
    private readonly IMapper _mapper;
    public PublisherRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Publisher>();
        _mapper = mapper;
    }

    public async Task<PaginatedResult<PublisherDto>> GetDtoPagedAsync(GetPagedPublishersQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Publisher> publishers = _dbSet;

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            publishers = publishers.Where(g => g.Name.Contains(request.Query));
        }

        var paginatedResult = await publishers.CreatePaginatedResultAsync<Publisher, PublisherDto>(request, _mapper, cancellationToken);

        return paginatedResult;
    }

    public async Task<PaginatedResult<PublisherForModerationDto>> GetDtoForModerationPagedAsync(GetPagedPublishersForModerationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Publisher> publishers = _dbSet;

        publishers = publishers.Where(p => p.Approved == request.Approved);

        var paginatedResult = await publishers.CreatePaginatedResultAsync<Publisher, PublisherForModerationDto>(request, _mapper, cancellationToken);

        return paginatedResult;
    }
}