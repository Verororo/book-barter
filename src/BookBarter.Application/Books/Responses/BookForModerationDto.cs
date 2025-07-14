
using BookBarter.Application.Common.Responses;
using BookBarter.Application.Genres.Responses;
using BookBarter.Application.Publishers.Responses;
using BookBarter.Application.Users.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Books.Responses;

public class BookForModerationDto
{
    public int Id { get; set; }
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public required DateOnly PublicationDate { get; set; }
    public required DateTime AddedToDatabaseDate { get; set; }
    public bool Approved { get; set; }
    public GenreDto Genre { get; set; } = default!;
    public PublisherDto Publisher { get; set; } = default!;

    public List<AuthorDto> Authors { get; set; } = [];
    public List<BookRelationshipWithUserDto> OwnedByUsers { get; set; } = [];
    public List<BookRelationshipWithUserDto> WantedByUsers { get; set; } = [];
}
