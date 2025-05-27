
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;

public class OwnedBookRepository : IOwnedBookRepository
{
    private readonly List<OwnedBook> _userBooks = new();

    public OwnedBook Create(OwnedBook userBook)
    {
        _userBooks.Add(userBook);
        return userBook;
    }
    public IList<OwnedBook> GetAll()
    {
        return _userBooks.ToList();
    }
    public List<OwnedBook> GetByPredicate(Func<OwnedBook, bool> predicate)
    {
        return _userBooks.Where(predicate).ToList();
    }
    public void Delete(OwnedBook userBook)
    {
        _userBooks.RemoveAll(e => e.UserId == userBook.UserId && e.BookId == userBook.BookId);
    }
}