namespace BookBarter.Application.Common.Responses;

public class BookDto
{
    public int Id { get; set; }
    public string Isbn { get; set; } = default!; // split into two models. better to name them differently
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public bool Approved { get; set; }
    public string GenreName { get; set; } = default!;
    public string PublisherName { get; set; } = default!;
    public List<AuthorDto> Authors { get; set; } = [];
}