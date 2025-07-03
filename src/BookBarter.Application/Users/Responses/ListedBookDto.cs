namespace BookBarter.Application.Common.Responses;

public class ListedBookDto
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public bool Approved { get; set; }
    public string GenreName { get; set; } = default!;
    public string PublisherName { get; set; } = default!;
    public List<AuthorDto> Authors { get; set; } = [];
}