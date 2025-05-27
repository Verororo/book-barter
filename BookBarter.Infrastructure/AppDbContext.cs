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
    public DbSet<State> States { get; set; }
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
    }
}
