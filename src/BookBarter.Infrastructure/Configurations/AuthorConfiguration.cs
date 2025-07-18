
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
            .HasMaxLength(20);

        builder
            .Property(e => e.MiddleName)
            .HasMaxLength(20);

        builder
            .Property(e => e.LastName)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasIndex(a => new { a.FirstName, a.MiddleName, a.LastName })
            .HasFilter("[Approved] = 1")
            .IsUnique();
    }
}
