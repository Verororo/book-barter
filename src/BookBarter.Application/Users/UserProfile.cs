
using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        bool excludeUnapprovedBooks = true;

        CreateMap<User, UserDto>()
            .ForMember(x => x.OwnedBooks, o => o.MapFrom(x =>
                excludeUnapprovedBooks
                ? x.OwnedBooks.Where(ob => ob.Book.Approved)
                : x.OwnedBooks
            ))
            .ForMember(x => x.WantedBooks, o => o.MapFrom(x =>
                excludeUnapprovedBooks
                ? x.WantedBooks.Where(wb => wb.Book.Approved)
                : x.WantedBooks
            ));

        CreateMap<User, ListedUserDto>()
            .ForMember(x => x.CityNameWithCountry, o => o.MapFrom(x => x.City.Name + ", " + x.City.CountryName))
            .ForMember(x => x.OwnedBooks, o => o.MapFrom(x => x.OwnedBooks.Where(ob => ob.Book.Approved)))
            .ForMember(x => x.WantedBooks, o => o.MapFrom(x => x.WantedBooks.Where(wb => wb.Book.Approved)));

        CreateMap<Book, ListedBookDto>()
            .ForMember(x => x.GenreName, o => o.MapFrom(x => x.Genre.Name))
            .ForMember(x => x.PublisherName, o => o.MapFrom(x => x.Publisher.Name));

        CreateMap<OwnedBook, OwnedBookDto>()
            .ForMember(x => x.BookStateName, o => o.MapFrom(x => x.BookState.Name));

        CreateMap<WantedBook, WantedBookDto>();

        CreateMap<BookState, BookStateDto>();
    }
}
