using BookBarter.Application.Common.Responses;

namespace BookBarter.Application.Users.Responses;

public class OwnedBookDto
{
    public int Id { get; set; }
    public ListedBookDto Book { get; set; } = default!;
    public string BookStateName { get; set; } = default!;
    public DateTime AddedDate { get; set; }
}
