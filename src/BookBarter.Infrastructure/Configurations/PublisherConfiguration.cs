
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
            .HasFilter("[Approved] = 1")
            .IsUnique();
    }
}