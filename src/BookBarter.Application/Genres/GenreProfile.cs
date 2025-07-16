
using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Genres;

public class GenreProfile : Profile
{
    public GenreProfile() 
    {
        CreateMap<Genre, GenreDto>();
    }
}
