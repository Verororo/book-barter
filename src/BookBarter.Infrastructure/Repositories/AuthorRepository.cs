
using AutoMapper;
using BookBarter.Application.Authors.Queries;
using BookBarter.Application.Authors.Responses;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class AuthorRepository : IAuthorRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Author> _dbSet;
    private readonly IMapper _mapper;
    public AuthorRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Author>();
        _mapper = mapper;
    }

    public Task<PaginatedResult<AuthorDto>> GetDtoPagedAsync(GetPagedAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Author> authors = _dbSet;

        authors = authors.Where(a => a.Approved == true);

        if (request.IdsToSkip != null && request.IdsToSkip.Count != 0)
        {
            authors = authors.Where(a => !request.IdsToSkip.Contains(a.Id));
        }

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            authors = authors.Where(a => (a.FirstName + " " + a.LastName)
                .Contains(request.Query));
        }

        return authors.CreatePaginatedResultAsync<Author, AuthorDto>(request, _mapper, cancellationToken);
    }

    public Task<PaginatedResult<AuthorForModerationDto>> GetDtoForModerationPagedAsync(GetPagedAuthorsForModerationQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Author> authors = _dbSet;

        authors = authors.Where(a => a.Approved == request.Approved);

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            authors = authors.Where(a => (a.FirstName + " " + a.LastName)
                .Contains(request.Query));
        }

        return authors.CreatePaginatedResultAsync<Author, AuthorForModerationDto>(request, _mapper, cancellationToken);
    }
}
