
using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Domain.Entities;

public class WantedBook : Entity, IBookRelationship
{
    public Book Book { get; set; } = default!;
    public int BookId { get; set; }
    public User User { get; set; } = default!;
    public int UserId { get; set; }
    public DateTime AddedDate { get; set; }
}
