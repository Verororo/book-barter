using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookBarter.Domain.Entities;

public class BookAuthor
{
    public int AuthorId { get; set; }
    public int BookId { get; set; }
    
    public BookAuthor() { }
    public BookAuthor(int authorId, int bookId)
    {
        AuthorId = authorId;
        BookId = bookId;
    }
}
