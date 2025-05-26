
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Books.Responses
{
    public class BookDto
    {
        public int Id { get; set; }
        public required string Isbn { get; set; }
        public DateTime PublicationDate { get; set; }
        public required string Title { get; set; }
        public required string Genre { get; set; }
        public bool Approved { get; set; }

        public static BookDto FromBook(Book book)
        {
            return new BookDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                PublicationDate = book.PublicationDate,
                Title = book.Title,
                Genre = book.Genre,
                Approved = book.Approved
            };
        }
    }
}
