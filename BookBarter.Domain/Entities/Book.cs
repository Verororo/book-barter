
namespace BookBarter.Domain.Entities;
public class Book : Entity
{
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; }
    public bool Approved { get; set; }
    public Genre? Genre { get; set; }
    public int? GenreId { get; set; }
    public ICollection<Author> Authors { get; set; } = [];
    public ICollection<OwnedBook> OwnedByUsers { get; set; } = [];
    public ICollection<User> WantedByUsers { get; set; } = [];
}
