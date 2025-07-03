
using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Common;

public class CommonProfile : Profile
{
    public CommonProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(x => x.GenreName, o => o.MapFrom(x => x.Genre.Name))
            .ForMember(x => x.PublisherName, o => o.MapFrom(x => x.Publisher.Name));
        CreateMap<Author, AuthorDto>();
    }
}
