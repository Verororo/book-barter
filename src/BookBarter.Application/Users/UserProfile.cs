
using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDto>();
        CreateMap<User, ListedUserDto>()
            .ForMember(x => x.CityNameWithCountry, o => o.MapFrom(x => x.City.Name + ", " + x.City.CountryName));
        CreateMap<Book, ListedBookDto>()
            .ForMember(x => x.GenreName, o => o.MapFrom(x => x.Genre.Name))
            .ForMember(x => x.PublisherName, o => o.MapFrom(x => x.Publisher.Name));
        CreateMap<OwnedBook, OwnedBookDto>()
            .ForMember(x => x.BookStateName, o => o.MapFrom(x => x.BookState.Name));
        CreateMap<WantedBook, WantedBookDto>();
        CreateMap<BookState, BookStateDto>();
    }
}
