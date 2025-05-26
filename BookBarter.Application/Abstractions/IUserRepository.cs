using BookBarter.Domain.Entities;

namespace BookBarter.Application.Abstractions;
public interface IUserRepository
{
    User Create(User user);
    User? GetById(int id);
    IList<User> GetAll();
    List<User> GetByPredicate(Func<User, bool> predicate);
    void Delete(User user);
    User Update(User user);
    int GetNextId();
}
