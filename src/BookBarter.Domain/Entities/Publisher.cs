
using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Domain.Entities;

public class Publisher : Entity
{
    public string Name { get; set; } = default!;
    public bool Approved { get; set; }
    public required DateTime AddedDate { get; set; }

    public ICollection<Book> Books { get; set; } = [];
}
