using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Users.Responses;

public class OwnedBookDto
{
    public int Id { get; set; }
    public BookDto Book { get; set; } = default!;
    public BookStateDto BookState { get; set; } = default!;
}
