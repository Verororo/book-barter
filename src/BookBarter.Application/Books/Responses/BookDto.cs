
using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Books.Responses;

public class BookDto
{
    public int Id { get; set; }
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public bool Approved { get; set; }
    public string GenreName { get; set; } = default!;
    public string PublisherName { get; set; } = default!;
    public List<AuthorDto> Authors { get; set; } = [];
}
