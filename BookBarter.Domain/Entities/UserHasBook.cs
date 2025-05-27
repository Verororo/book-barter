namespace BookBarter.Domain.Entities;
public class UserHasBook : Entity
{
    public Book Book { get; set; } = default!;
    public int BookId { get; set; }
    public User User { get; set; } = default!;
    public int UserId { get; set; }
    public Bookstate BookState { get; set; } = default!;
    public int BookStateId { get; set; }
}