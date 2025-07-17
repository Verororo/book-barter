using AutoMapper;
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;
using UserBarter.Application.Users.Queries;

namespace BookBarter.Application.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        bool excludeUnapprovedBooks = true;
        GetPagedUsersQuery? query = null;

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.OwnedBooks, opt => opt.MapFrom(src =>
                excludeUnapprovedBooks
                ? src.OwnedBooks.Where(ob => ob.Book.Approved)
                : src.OwnedBooks
            ))
            .ForMember(dest => dest.WantedBooks, opt => opt.MapFrom(src =>
                excludeUnapprovedBooks
                ? src.WantedBooks.Where(wb => wb.Book.Approved)
                : src.WantedBooks
            ));
        /*
        CreateMap<User, ListedUserDto>()
            .ForMember(x => x.CityNameWithCountry, o => o.MapFrom(x => x.City.Name + ", " + x.City.CountryName))
            .ForMember(x => x.OwnedBooks, o => o.MapFrom(x => x.OwnedBooks.Where(ob => ob.Book.Approved)))
            .ForMember(x => x.WantedBooks, o => o.MapFrom(x => x.WantedBooks.Where(wb => wb.Book.Approved)));
        */
        CreateMap<User, ListedUserDto>()
            .ForMember(dest => dest.CityNameWithCountry, opt => opt.MapFrom(src => 
                src.City.Name + ", " + src.City.CountryName))
            .ForMember(dest => dest.OwnedBooks, opt => opt.MapFrom((src, dest, destMember, context) =>
            {
                var ownedBooksIds = query?.OwnedBooksIds ?? new List<int>();

                return src.OwnedBooks
                    .Where(ob => ob.Book.Approved)
                    .OrderByDescending(ob => ownedBooksIds.Contains(ob.BookId))
                    .ThenBy(ob => ob.AddedDate);
            }))
            .ForMember(x => x.WantedBooks, o => o.MapFrom((src, dest, destMember, context) =>
            {
                var wantedBooksIds = query?.WantedBooksIds ?? new List<int>();

                return src.WantedBooks
                    .Where(wb => wb.Book.Approved)
                    .OrderByDescending(wb => wantedBooksIds.Contains(wb.BookId))
                    .ThenBy(wb => wb.AddedDate);
            }));

        CreateMap<Book, ListedBookDto>()
            .ForMember(dest => dest.GenreName, opt => opt.MapFrom(src => src.Genre.Name))
            .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.Name));

        CreateMap<OwnedBook, OwnedBookDto>()
            .ForMember(dest => dest.BookStateName, opt => opt.MapFrom(src => src.BookState.Name));

        CreateMap<WantedBook, WantedBookDto>();

        CreateMap<BookState, BookStateDto>();
    }
}