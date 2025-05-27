using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class AuthorEntityTypeConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        builder
            .Property(e => e.FirstName)
            .HasMaxLength(20)
            .IsRequired(true);

        builder
            .Property(e => e.MiddleName)
            .HasMaxLength(20)
            .IsRequired(false);

        builder
            .Property(e => e.LastName)
            .HasMaxLength(20)
            .IsRequired(true);
    }
}
