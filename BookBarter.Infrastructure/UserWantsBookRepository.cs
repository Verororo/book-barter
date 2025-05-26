
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;

public class WantedUserBookRepository : IUserWantsBookRepository
{
    private readonly List<UserWantsBook> _wantedUserBooks = new();

    public UserWantsBook Create(UserWantsBook wantedUserBook)
    {
        _wantedUserBooks.Add(wantedUserBook);
        return wantedUserBook;
    }
    public IList<UserWantsBook> GetAll()
    {
        return _wantedUserBooks.ToList();
    }
    public List<UserWantsBook> GetByPredicate(Func<UserWantsBook, bool> predicate)
    {
        return _wantedUserBooks.Where(predicate).ToList();
    }
    public void Delete(UserWantsBook wantedUserBook)
    {
        _wantedUserBooks.RemoveAll(e => e.UserId == wantedUserBook.UserId && e.BookId == wantedUserBook.BookId);
    }
}