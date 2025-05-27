
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(e => e.Email)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(e => e.About)
            .HasMaxLength(500);

        builder
            .Property(e => e.City)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .HasIndex(e => e.Email)
            .IsUnique();

        builder
            .HasMany(u => u.WantedBooks)
            .WithMany(b => b.WantedByUsers)
            .UsingEntity(uwb => uwb.ToTable("WantedBooks"));
    }
}
