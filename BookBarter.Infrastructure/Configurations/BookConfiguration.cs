
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .Property(e => e.Isbn)
            .HasMaxLength(13)
            .IsRequired();

        builder
            .Property(e => e.Title)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .HasIndex(e => e.Isbn)
            .IsUnique();

        // FOR TESTING
        builder
            .HasData(
                new Book
                {
                    Id = 1,
                    Isbn = "9781400079988",
                    Title = "War and Peace",
                    PublicationDate = DateOnly.Parse("2008-12-02"),
                    GenreId = 1,
                    PublisherId = 1
                },
                new Book
                {
                    Id = 2,
                    Isbn = "9781613820254",
                    Title = "Les Miserables",
                    PublicationDate = DateOnly.Parse("2012-09-26"),
                    GenreId = 1,
                    PublisherId = 2
                }
            );

        builder
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(t => t
                .HasData(new[]
                    {
                        new { AuthorsId = 1, BooksId = 1 },
                        new { AuthorsId = 2, BooksId = 2 }
                    }
                )
            );
    }
}
