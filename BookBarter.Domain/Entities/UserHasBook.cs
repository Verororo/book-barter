using BookBarter.Domain.Enums;
namespace BookBarter.Domain.Entities;
public class UserHasBook : Entity
{
    public int BookId { get; set; }
    public int UserId { get; set; }

    public State BookState { get; set; }

    public UserHasBook(int userId, int bookId, State state)
    {
        UserId = userId;
        BookId = bookId;
        BookState = state;
    }
}