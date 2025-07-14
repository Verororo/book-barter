using BookBarter.Application.Books.Responses;

namespace BookBarter.Application.Publishers.Responses;

public class PublisherForModerationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public bool Approved { get; set; }

    public List<BookDto> Books { get; set; } = [];
}
