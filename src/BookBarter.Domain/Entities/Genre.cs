using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Domain.Entities;

public class Genre : Entity
{
    public string Name { get; set; } = default!;
}
