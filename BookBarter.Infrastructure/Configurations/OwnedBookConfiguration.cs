
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class OwnedBookConfiguration : IEntityTypeConfiguration<OwnedBook>
{
    public void Configure(EntityTypeBuilder<OwnedBook> builder)
    {
        builder
            .HasIndex(e => new { e.UserId, e.BookId })
            .IsUnique();

        builder
            .HasOne(uhb => uhb.Book)
            .WithMany(b => b.OwnedByUsers)
            .HasForeignKey(uhb => uhb.BookId);

        builder
            .HasOne(uhb => uhb.User)
            .WithMany(u => u.OwnedBooks)
            .HasForeignKey(uhb => uhb.UserId);
    }
}
