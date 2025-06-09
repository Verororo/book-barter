
namespace BookBarter.Domain.Entities;
public class User : Entity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string? About { get; set; }
    public string City { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<OwnedBook> OwnedBooks { get; set; } = [];
    public ICollection<Book> WantedBooks { get; set; } = []; 
    public ICollection<Message> SentMessages { get; set; } = [];
    public ICollection<Message> ReceivedMessages { get; set; } = [];
}