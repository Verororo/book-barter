
using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Users.Responses;

public class WantedBookDto
{
    public int Id { get; set; }
    public BookDto Book { get; set; } = default!;
    public DateTime AddedDate { get; set; }
}
