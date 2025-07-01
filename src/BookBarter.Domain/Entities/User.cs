
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Domain.Entities;
public class User : IdentityUser<int>
{
    public string? About { get; set; }
    public string City { get; set; } = default!; // use a pre-defined list of cities?
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<OwnedBook> OwnedBooks { get; set; } = [];
    public ICollection<Book> WantedBooks { get; set; } = []; 
    public ICollection<Message> SentMessages { get; set; } = [];
    public ICollection<Message> ReceivedMessages { get; set; } = [];
}