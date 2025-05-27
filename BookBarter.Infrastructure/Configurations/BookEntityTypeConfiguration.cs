using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class BookEntityTypeConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        builder
            .Property(e => e.Isbn)
            .HasMaxLength(13); // indexes?

        builder
            .Property(e => e.Title)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(e => e.PublicationDate)
            .IsRequired(true);

        builder
            .Property(e => e.GenreId)
            .IsRequired(false);

        builder
            .Property(e => e.Approved)
            .HasDefaultValue(false);
    }
}
