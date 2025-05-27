using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Configurations;

namespace BookBarter.Infrastructure;

public class AppDbContext : DbContext
{
    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Bookstate> Bookstates { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserHasBook> UserHasBooks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=BookBarter;Trusted_Connection=True")
            .LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name },
                LogLevel.Information);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorEntityTypeConfiguration).Assembly);

        // UserWantsBooks relationship
        modelBuilder.Entity<User>()
            .HasMany(u => u.BooksWanted)
            .WithMany(b => b.UsersWantedBy)
            .UsingEntity(uwb => uwb.ToTable("UserWantsBook"));

        // UserHasBooks relationship
        modelBuilder.Entity<UserHasBook>()
            .HasOne(uhb => uhb.Book)
            .WithMany(b => b.UserHasBooks)
            .HasForeignKey(uhb => uhb.BookId);

        modelBuilder.Entity<UserHasBook>()
            .HasOne(uhb => uhb.User)
            .WithMany(u => u.UserHasBooks)
            .HasForeignKey(uhb => uhb.UserId);

        // User and message relationship
        modelBuilder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany(u => u.SentMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict); // is restrict the best option here?

        modelBuilder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
