
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Configurations;

public class PublisherConfiguration : IEntityTypeConfiguration<Publisher>
{
    public void Configure(EntityTypeBuilder<Publisher> builder)
    {
        builder
            .Property(e => e.Name)
            .HasMaxLength(30)
            .IsRequired();

        builder
            .HasIndex(e => e.Name)
            .IsUnique();

        // FOR TESTING
        builder
            .HasData(
                new Publisher { Id = 1, Name = "Vintage" },
                new Publisher { Id = 2, Name = "Simon & Brown" },
                new Publisher { Id = 3, Name = "Global Publishers" }
            );
    }
}