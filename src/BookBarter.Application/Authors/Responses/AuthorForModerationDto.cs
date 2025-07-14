using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Authors.Responses;

public class AuthorForModerationDto
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public bool Approved { get; set; }

    public List<BookDto> Books { get; set; } = [];
}
