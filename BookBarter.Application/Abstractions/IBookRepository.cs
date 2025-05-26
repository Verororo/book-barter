using BookBarter.Domain.Entities;

namespace BookBarter.Application.Abstractions;
public interface IBookRepository
{
    Book Create(Book book);
    Book? GetById(int id);
    IList<Book> GetAll();
    List<Book> GetByPredicate(Func<Book, bool> predicate);
    void Delete(Book book);
    Book Update(Book book);
    int GetNextId();
}
