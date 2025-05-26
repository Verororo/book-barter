
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;

public class UserBookRepository : IUserHasBookRepository
{
    private readonly List<UserHasBook> _userBooks = new();

    public UserHasBook Create(UserHasBook userBook)
    {
        _userBooks.Add(userBook);
        return userBook;
    }
    public IList<UserHasBook> GetAll()
    {
        return _userBooks.ToList();
    }
    public List<UserHasBook> GetByPredicate(Func<UserHasBook, bool> predicate)
    {
        return _userBooks.Where(predicate).ToList();
    }
    public void Delete(UserHasBook userBook)
    {
        _userBooks.RemoveAll(e => e.UserId == userBook.UserId && e.BookId == userBook.BookId);
    }
}