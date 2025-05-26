namespace BookBarter.Domain.Entities;
public class Book : Entity
{
    public string Isbn { get; set; }
    public string Title { get; set; }
    public DateTime PublicationDate {  get; set; }
    public string Genre { get; set; }
    public bool Approved { get; set; }

    public Book(string isbn, string title, string genre)
    {
        Isbn = isbn;
        Title = title;
        Genre = genre;
    }
    public Book(int id, string isbn, string title, string genre)
    {
        Id = id;
        Isbn = isbn;
        Title = title;
        Genre = genre;
    }
}
