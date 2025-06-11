
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.IntegrationTests.Helpers;

public class DbContextBuilder : IDisposable
{
    private readonly AppDbContext _dbContext;

    public DbContextBuilder(string dbName = "TestDatabase")
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

        var context = new AppDbContext(options);

        _dbContext = context;
    }

    public AppDbContext GetContext() { 
        _dbContext.Database.EnsureCreated();
        return _dbContext; 
    }

    public void SeedBooks(int amount = 1)
    {
        var books = new List<Book>();

        for (int i = 0; i < amount; i++)
        {
            var id = i + 1;

            books.Add(new Book
            {
                Isbn = $"{id}",
                Id = id,
                Title = $"Book-{id}",
                PublicationDate = DateOnly.MinValue
            });
        }

        _dbContext.AddRange(books);
        _dbContext.SaveChanges();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}
