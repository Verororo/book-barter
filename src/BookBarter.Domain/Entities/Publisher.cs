
namespace BookBarter.Domain.Entities;

public class Publisher : Entity
{
    public string Name { get; set; } = default!;
    public bool Approved { get; set; }
}
