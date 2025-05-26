using BookBarter.Domain.Enums;
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Responses
{
    public class UserHasBookDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public State BookState { get; set; }

        public static UserHasBookDto FromUserBook(UserHasBook userBook)
        {
            return new UserHasBookDto
            {
                BookId = userBook.BookId,
                UserId = userBook.UserId,
                BookState = userBook.BookState
            };
        }
    }
}
