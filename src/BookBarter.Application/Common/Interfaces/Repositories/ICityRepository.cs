using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Application.Cities.Queries;
using BookBarter.Application.Cities.Responses;
using BookBarter.Application.Common.Models;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common.Interfaces.Repositories;

public interface ICityRepository
{
    public Task<PaginatedResult<CityDto>> GetDtoPagedAsync(GetPagedCitiesQuery request,
        CancellationToken cancellationToken);
}
