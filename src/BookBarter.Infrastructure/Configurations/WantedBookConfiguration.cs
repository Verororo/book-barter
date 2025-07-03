
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.Infrastructure.Configurations;

public class WantedBookConfiguration : IEntityTypeConfiguration<WantedBook>
{
    public void Configure(EntityTypeBuilder<WantedBook> builder)
    {
        builder
            .HasIndex(e => new { e.UserId, e.BookId })
            .IsUnique();

        builder
            .HasOne(uwb => uwb.Book)
            .WithMany(b => b.WantedByUsers)
            .HasForeignKey(uwb => uwb.BookId);

        builder
            .HasOne(uwb => uwb.User)
            .WithMany(u => u.WantedBooks)
            .HasForeignKey(uwb => uwb.UserId);
    }
}

