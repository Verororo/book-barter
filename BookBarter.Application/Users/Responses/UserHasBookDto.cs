using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Responses
{
    public class UserHasBookDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int BookStateId { get; set; }

        public static UserHasBookDto FromUserBook(UserHasBook userBook)
        {
            return new UserHasBookDto
            {
                BookId = userBook.BookId,
                UserId = userBook.UserId,
                BookStateId = userBook.BookStateId
            };
        }
    }
}
