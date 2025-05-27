using BookBarter.Domain.Entities;

namespace BookBarter.Application.Abstractions;
public interface IOwnedBookRepository
{
    OwnedBook Create(OwnedBook userBook);
    IList<OwnedBook> GetAll();
    List<OwnedBook> GetByPredicate(Func<OwnedBook, bool> predicate);
    void Delete(OwnedBook userBook);
}
