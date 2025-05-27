using BookBarter.Domain.Entities;

namespace BookBarter.Application.Users.Responses
{
    public class OwnedBookDto
    {
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int BookStateId { get; set; }

        public static OwnedBookDto FromUserBook(OwnedBook userBook)
        {
            return new OwnedBookDto
            {
                BookId = userBook.BookId,
                UserId = userBook.UserId,
                BookStateId = userBook.BookStateId
            };
        }
    }
}
