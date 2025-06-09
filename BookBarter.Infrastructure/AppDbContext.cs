
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure.Configurations;

namespace BookBarter.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions options) : base(options) { }
    public AppDbContext() { }

    public DbSet<Author> Authors { get; set; }
    public DbSet<BookState> BookStates { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Message> Messages { get; set; }
    public DbSet<OwnedBook> OwnedBooks { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthorConfiguration).Assembly);
    }
}
