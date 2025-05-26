using BookBarter.Domain.Entities;

namespace BookBarter.Application.Abstractions;
public interface IUserHasBookRepository
{
    UserHasBook Create(UserHasBook userBook);
    IList<UserHasBook> GetAll();
    List<UserHasBook> GetByPredicate(Func<UserHasBook, bool> predicate);
    void Delete(UserHasBook userBook);
}
