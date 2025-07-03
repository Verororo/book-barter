
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Domain.Entities;
public class User : IdentityUser<int>, IEntity
{
    public string? About { get; set; }
    public City City { get; set; } = default!;
    public int? CityId { get; set; }
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<OwnedBook> OwnedBooks { get; set; } = [];
    public ICollection<Book> WantedBooks { get; set; } = []; 
    public ICollection<Message> SentMessages { get; set; } = [];
    public ICollection<Message> ReceivedMessages { get; set; } = [];
}