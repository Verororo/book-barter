
namespace BookBarter.Domain.Entities;

public class City : Entity
{
    public string Name { get; set; } = default!;
    public string CountryName { get; set; } = default!;
    public ICollection<User> Users { get; set; } = [];
}
