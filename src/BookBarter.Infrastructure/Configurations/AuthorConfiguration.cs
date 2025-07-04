
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .Property(e => e.FirstName)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .Property(e => e.LastName)
            .HasMaxLength(20)
            .IsRequired();

        // FOR TESTING
        /*
        builder
            .HasData(
                new Author { Id = 1, FirstName = "Leo", LastName = "Tolstoy" },
                new Author { Id = 2, FirstName = "Victor", LastName = "Hugo" }
                );
        */
    }
}
