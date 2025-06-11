using AutoMapper;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Books;

public class BookProfile : Profile
{
    public BookProfile() {
        CreateMap<Book, BookDto>();
        CreateMap<Author, AuthorDto>();
        CreateMap<Genre, GenreDto>();
        CreateMap<Publisher, PublisherDto>();
    }
}
