namespace BookBarter.Domain.Entities;
public class OwnedBook : Entity
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int BookStateId { get; set; }
    public Book Book { get; set; } = default!;
    public User User { get; set; } = default!;
    public BookState BookState { get; set; } = default!;
}