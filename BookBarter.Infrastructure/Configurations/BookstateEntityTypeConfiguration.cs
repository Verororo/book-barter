using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class BookstateEntityTypeConfiguration : IEntityTypeConfiguration<Bookstate>
{
    public void Configure(EntityTypeBuilder<Bookstate> builder)
    {
        builder
            .Property(e => e.Name)
            .HasMaxLength(20)
            .IsRequired(true);

        builder
            .HasData(
                new Bookstate { Id = 1, Name = "Old" },
                new Bookstate { Id = 2, Name = "Medium" },
                new Bookstate { Id = 3, Name = "New" }
                );
    }
}
