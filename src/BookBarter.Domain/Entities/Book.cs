
using BookBarter.Domain.Entities.Abstractions;

namespace BookBarter.Domain.Entities;
public class Book : Entity
{
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public required DateOnly PublicationDate { get; set; }
    public required DateTime AddedToDatabaseDate { get; set; }
    public bool Approved { get; set; }
    public Genre Genre { get; set; } = default!;
    public int GenreId { get; set; }
    public Publisher Publisher { get; set; } = default!;
    public int PublisherId { get; set; }

    public ICollection<Author> Authors { get; set; } = [];
    public ICollection<OwnedBook> OwnedByUsers { get; set; } = [];
    public ICollection<WantedBook> WantedByUsers { get; set; } = [];
}
