namespace BookBarter.Domain.Entities;
public class UserHasBook : Entity
{
    public int BookId { get; set; }
    public int UserId { get; set; }
    public int BookStateId { get; set; }
}