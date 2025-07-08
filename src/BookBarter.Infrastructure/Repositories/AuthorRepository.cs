
using AutoMapper;
using BookBarter.Application.Authors.Queries;
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

    public async Task<PaginatedResult<AuthorDto>> GetDtoPagedAsync(GetPagedAuthorsQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Author> authors = _dbSet;

        //authors = authors.Where(a => a.Approved == true);

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            authors = authors.Where(a => (a.FirstName + " " + a.LastName)
                .Contains(request.Query));
        }

        var paginatedResult = await authors.CreatePaginatedResultAsync<Author, AuthorDto>(request, _mapper, cancellationToken);

        return paginatedResult;
    }
}
