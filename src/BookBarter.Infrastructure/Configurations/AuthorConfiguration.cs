
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
            .HasMaxLength(30);

        builder
            .Property(e => e.MiddleName)
            .HasMaxLength(30);

        builder
            .Property(e => e.LastName)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .HasIndex(a => new { a.FirstName, a.MiddleName, a.LastName })
            .HasFilter("[Approved] = 1")
            .IsUnique();
    }
}
