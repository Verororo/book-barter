using System.Reflection.Metadata;

namespace BookBarter.Domain.Entities;
public class UserWantsBook
{
    public int UserId { get; set; }
    public int BookId { get; set; }

    public UserWantsBook() { }
    public UserWantsBook(int userId, int bookId)
    {
        UserId = userId;
        BookId = bookId;
    }
}