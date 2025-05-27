using System.ComponentModel.DataAnnotations;

namespace BookBarter.Domain.Entities;
public class Book : Entity
{
    public string Isbn { get; set; } = default!;
    public string Title { get; set; } = default!;
    public DateOnly PublicationDate { get; set; } // is it right to use DateOnly?
    public int? GenreId { get; set; } // 'null' means no genre is set yet
    public bool Approved { get; set; }
}
