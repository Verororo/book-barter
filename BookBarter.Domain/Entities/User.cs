using System.ComponentModel.DataAnnotations;

namespace BookBarter.Domain.Entities;
public class User : Entity
{
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string About { get; set; } = string.Empty;
    public string City { get; set; } = default!;
    public DateTime RegistrationDate { get; set; }
    public DateTime? LastOnlineDate { get; set; }
    public ICollection<UserHasBook> UserHasBooks { get; set; } = default!;
    public ICollection<Book> BooksWanted { get; set; } = default!; // UserWantsBook
    public ICollection<Message> SentMessages { get; set; } = default!;
    public ICollection<Message> ReceivedMessages { get; set; } = default!;
}