
using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Domain.Entities;

public class BookState : Entity
{
    public string Name { get; set; } = default!;
}
