using AutoMapper;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Books;

public class BookProfile : Profile
{
    public BookProfile()
    {
        CreateMap<Book, BookDto>()
            .ForMember(x => x.GenreName, o => o.MapFrom(x => x.Genre.Name))
            .ForMember(x => x.PublisherName, o => o.MapFrom(x => x.Publisher.Name));

        CreateMap<Book, BookForModerationDto>();

        CreateMap<WantedBook, BookRelationshipWithUserDto>();

        CreateMap<OwnedBook, BookRelationshipWithUserDto>();

        CreateMap<User, CollapsedUserDto>();
    }
}

