
namespace BookBarter.Domain.Entities;

public class Author : Entity
{
    public string FirstName { get; set; } = default!;
    public string? MiddleName { get; set; }
    public string LastName { get; set; } = default!;
    public bool Approved { get; set; }
    public ICollection<Book> Books { get; set; } = [];
}
