using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class StateEntityTypeConfiguration : IEntityTypeConfiguration<State>
{
    public void Configure(EntityTypeBuilder<State> builder)
    {
        builder
            .Property(e => e.Name)
            .HasMaxLength(20)
            .IsRequired(true);

        builder
            .HasData(
                new State { Id = 1, Name = "Old" },
                new State { Id = 2, Name = "Medium" },
                new State { Id = 3, Name = "New" }
                );
    }
}
