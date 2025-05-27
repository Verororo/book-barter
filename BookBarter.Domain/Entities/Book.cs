using System.ComponentModel.DataAnnotations;

namespace BookBarter.Domain.Entities;
public class Book : Entity
{
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; } // is it right to use DateOnly?
    public bool Approved { get; set; }
    public Genre Genre { get; set; } = default!; // 'null' means no genre is set yet
    public int? GenreId { get; set; } // 'null' means no genre is set yet
    public ICollection<Author> Authors { get; set; } = default!;
    public ICollection<UserHasBook> UserHasBooks { get; set; } = default!;
    public ICollection<User> UsersWantedBy { get; set; } = default!;
}
