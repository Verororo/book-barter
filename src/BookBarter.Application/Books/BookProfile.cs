using AutoMapper;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Books;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name));

        CreateMap<Book, BookForModerationDto>();

        CreateMap<WantedBook, BookRelationshipWithUserDto>();

        CreateMap<OwnedBook, BookRelationshipWithUserDto>();

        CreateMap<User, CollapsedUserDto>();
    }
}

