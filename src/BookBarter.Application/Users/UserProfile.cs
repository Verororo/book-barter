
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
        CreateMap<User, ListedUserDto>();
        CreateMap<OwnedBook, OwnedBookDto>();
        CreateMap<WantedBook, WantedBookDto>();
        CreateMap<BookState, BookStateDto>();
    }
}
