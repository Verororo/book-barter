
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class BookStateConfiguration : IEntityTypeConfiguration<BookState>
{
    public void Configure(EntityTypeBuilder<BookState> builder)
    {
        builder
            .Property(e => e.Name)
            .HasMaxLength(20)
            .IsRequired();

        builder
            .HasData(
                new BookState { Id = 1, Name = "Old" },
                new BookState { Id = 2, Name = "Medium" },
                new BookState { Id = 3, Name = "New" }
                );
    }
}
