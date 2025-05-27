
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
            .IsRequired(true);

        builder
            .Property(e => e.MiddleName)
            .HasMaxLength(20)
            .IsRequired(false);

        builder
            .Property(e => e.LastName)
            .HasMaxLength(20)
            .IsRequired(true);
    }
}
