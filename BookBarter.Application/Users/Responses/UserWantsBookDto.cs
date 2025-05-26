using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Responses;

public class UserWantsBookDto
{
    public int BookId { get; set; }
    public int UserId { get; set; }

    public static UserWantsBookDto FromWantedUserBook(UserWantsBook wantedUserBook)
    {
        return new UserWantsBookDto
        {
            BookId = wantedUserBook.BookId,
            UserId = wantedUserBook.UserId
        };
    }
}
