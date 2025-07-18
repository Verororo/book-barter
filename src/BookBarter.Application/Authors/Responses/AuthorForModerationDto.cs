using BookBarter.Application.Books.Responses;

namespace BookBarter.Application.Authors.Responses;

public class AuthorForModerationDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public bool Approved { get; set; }
    public DateTime AddedDate { get; set; }

    public List<BookDto> Books { get; set; } = [];
}
