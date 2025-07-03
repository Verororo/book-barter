
using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateMap<Author, AuthorDto>();
    }
}
