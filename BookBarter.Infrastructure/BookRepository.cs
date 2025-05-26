
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;

public class BookRepository : IBookRepository
{
    private readonly List<Book> _books = new();

    public Book Create(Book book)
    {
        _books.Add(book);
        return book;
    }
    public Book GetById(int id)
    {
        return _books.FirstOrDefault(e => e.Id == id);
    }
    public IList<Book> GetAll()
    {
        return _books.ToList();
    }
    public List<Book> GetByPredicate(Func<Book, bool> predicate)
    {
        return _books.Where(predicate).ToList();
    }
    public void Delete(Book Book)
    {
        _books.RemoveAll(e => e.Id == Book.Id);
    }
    public Book Update(Book book)
    {
        var existingBook = GetById(book.Id);
        if (existingBook != null)
        {
            _books.Remove(existingBook);
            _books.Add(book);
        }
        return book;
    }
    public int GetNextId()
    {
        if (_books.Count == 0) return 1;
        var lastId = _books.Max(a => a.Id);
        return lastId + 1;
    }
}
