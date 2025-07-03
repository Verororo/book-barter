
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(e => e.About)
            .HasMaxLength(500);

        builder
            .HasOne(u => u.City)
            .WithMany(c => c.Users)
            .HasForeignKey(u => u.CityId)
            .OnDelete(DeleteBehavior.SetNull);

        builder
            .HasIndex(e => e.Email)
            .IsUnique();

        builder
            .HasMany(u => u.WantedBooks)
            .WithMany(b => b.WantedByUsers)
            .UsingEntity(uwb => uwb.ToTable("WantedBooks"));
    }
}
