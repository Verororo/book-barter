using BookBarter.Domain.Entities;

namespace BookBarter.Application.Abstractions;
public interface IUserWantsBookRepository
{
    UserWantsBook Create(UserWantsBook wanterUserBook);
    IList<UserWantsBook> GetAll();
    List<UserWantsBook> GetByPredicate(Func<UserWantsBook, bool> predicate);
    void Delete(UserWantsBook wantedUserBook);
}
