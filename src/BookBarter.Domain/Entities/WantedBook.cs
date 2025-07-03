
namespace BookBarter.Domain.Entities;

public class WantedBook
{
    public Book Book { get; set; } = default!;
    public int BookId { get; set; }
    public User User { get; set; } = default!;
    public int UserId { get; set; }
    public DateTime AddedDate { get; set; }
    public int BookStateId { get; set; }
}
