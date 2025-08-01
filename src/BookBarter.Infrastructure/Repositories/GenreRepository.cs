﻿
using AutoMapper;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Genres.Queries;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Repositories;

public class GenreRepository : IGenreRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Genre> _dbSet;
    private readonly IMapper _mapper;
    public GenreRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _dbSet = _context.Set<Genre>();
        _mapper = mapper;
    }

    public Task<PaginatedResult<GenreDto>> GetDtoPagedAsync(GetPagedGenresQuery request,
        CancellationToken cancellationToken)
    {
        IQueryable<Genre> genres = _dbSet;

        if (!string.IsNullOrWhiteSpace(request.Query))
        {
            genres = genres.Where(g => g.Name.Contains(request.Query));
        }

        return genres.CreatePaginatedResultAsync<Genre, GenreDto>(request, _mapper, cancellationToken);
    }
}
