
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class GenreConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .Property(e => e.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .HasIndex(e => e.Name)
            .IsUnique();

        builder
            .HasData(new Genre { Id = 1, Name = "Classics" });
    }
}
