using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Application.Authors.Queries;
using BookBarter.Application.Common.Models;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Genres.Queries;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface IGenreRepository
{
    public Task<PaginatedResult<GenreDto>> GetDtoPagedAsync(GetPagedGenresQuery request,
        CancellationToken cancellationToken);
}
