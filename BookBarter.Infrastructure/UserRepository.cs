
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;

public class UserRepository : IUserRepository
{
    private readonly List<User> _users = new();

    public User Create(User user)
    {
        _users.Add(user);
        return user;
    }
    public User GetById(int id)
    {
        return _users.FirstOrDefault(e => e.Id == id);
    }
    public IList<User> GetAll()
    {
        return _users.ToList();
    }
    public List<User> GetByPredicate(Func<User, bool> predicate)
    {
        return _users.Where(predicate).ToList();
    }
    public void Delete(User user)
    {
        _users.RemoveAll(e => e.Id == user.Id);
    }
    public User Update(User user)
    {
        var existinguser = GetById(user.Id);
        if (existinguser != null)
        {
            _users.Remove(existinguser);
            _users.Add(user);
        }
        return user;
    }
    public int GetNextId()
    {
        if (_users.Count == 0) return 1;
        var lastId = _users.Max(a => a.Id);
        return lastId + 1;
    }
}
