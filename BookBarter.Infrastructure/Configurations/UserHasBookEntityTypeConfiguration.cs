using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class UserHasBookEntityTypeConfiguration : IEntityTypeConfiguration<UserHasBook>
{
    public void Configure(EntityTypeBuilder<UserHasBook> builder)
    {
        builder
            .Property(e => e.BookId)
            .IsRequired(true);

        builder
            .Property(e => e.UserId)
            .IsRequired(true);

        builder
            .Property(e => e.BookStateId)
            .IsRequired(true);
    }
}
