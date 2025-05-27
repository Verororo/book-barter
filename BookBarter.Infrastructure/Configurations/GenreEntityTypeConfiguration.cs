using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class GenreEntityTypeConfiguration : IEntityTypeConfiguration<Genre>
{
    public void Configure(EntityTypeBuilder<Genre> builder)
    {
        builder
            .Property(e => e.Name)
            .HasMaxLength(20)
            .IsRequired(true);
    }
}
