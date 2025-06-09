
namespace BookBarter.Application.Books.Responses;

public class BookDto
{
    public int Id { get; set; }
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public bool Approved { get; set; }
    public int GenreId { get; set; } // name
    public int PublisherId { get; set; }
    public List<AuthorDto> Authors { get; set; } = default!;
}