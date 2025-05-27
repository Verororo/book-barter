
using BookBarter.Domain.Entities;

namespace BookBarter.Application.Books.Responses
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Isbn { get; set; } = default!;
        public string Title { get; set; } = default!;
        public DateOnly PublicationDate { get; set; }
        public int? GenreId { get; set; }
        public bool Approved { get; set; }

        public static BookDto FromBook(Book book)
        {
            if (book.Isbn == null || book.Title == null)
            {
                throw new ArgumentNullException(nameof(book));
            }

            return new BookDto
            {
                Id = book.Id,
                Isbn = book.Isbn,
                PublicationDate = book.PublicationDate,
                Title = book.Title,
                GenreId = book.GenreId,
                Approved = book.Approved
            };
        }
    }
}
