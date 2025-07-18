using AutoMapper;
using BookBarter.Application.Authors.Responses;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Authors;

public class AuthorProfile : Profile
{
    public AuthorProfile() 
    {
        CreateMap<Author, AuthorDto>();

        CreateMap<Author, AuthorForModerationDto>();
    }
}
