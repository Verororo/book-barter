using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookBarter.Infrastructure.Configurations;

public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder
            .Property(e => e.SenderId)
            .IsRequired(true);

        builder
            .Property(e => e.ReceiverId)
            .IsRequired(true);

        builder
            .Property(e => e.Body)
            .HasMaxLength(500) // not 255?
            .IsRequired(true);

        builder
            .Property(e => e.SentTime)
            .IsRequired(true); // default date is set by the backend?

        builder
            .Property(e => e.EditTime)
            .IsRequired(false);
    }
}
