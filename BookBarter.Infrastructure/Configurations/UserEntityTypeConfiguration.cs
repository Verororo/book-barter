using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .Property(e => e.Email)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(e => e.Name)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(e => e.About)
            .HasMaxLength(500)
            .IsRequired(false);

        builder
            .Property(e => e.City)
            .HasMaxLength(50)
            .IsRequired(true);

        builder
            .Property(e => e.RegistrationDate)
            .IsRequired(true); // default value is set on the backend level

        builder
            .Property(e => e.LastOnlineDate)
            .IsRequired(false);
    }
}
