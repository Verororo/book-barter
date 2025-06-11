
using BookBarter.API.Controllers;
using BookBarter.Application.Books;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Responses;
using BookBarter.Application.Extensions;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure;
using BookBarter.Infrastructure.Extensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.IntegrationTests
{
    public class BooksControllerTests : IDisposable
    {
        private readonly IServiceProvider _serviceProvider;

        public BooksControllerTests()
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddApplication();
            serviceCollection.AddInfrastructure();
            serviceCollection.AddAutoMapper(typeof(BookProfile).Assembly);

            serviceCollection.AddDbContext<AppDbContext>(optionBuilder =>
            {
                optionBuilder.UseInMemoryDatabase($"TestDb - {Guid.NewGuid()}");
            });

            _serviceProvider = serviceCollection.BuildServiceProvider();

            SeedContext();
        }

        public void Dispose()
        {
            if (_serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }

        [Fact]
        public async Task BooksController_GetBookById_BookIsReturned()
        {
            // Arrange
            int testId = 1;

            var mediator = _serviceProvider.GetService<IMediator>();
            var controller = new BooksController(mediator!);
            
            // Act
            BookDto? response = await controller.GetBookById(testId, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(response.Id, testId);
        }

        [Fact]
        public async Task BooksController_CreateBook_BookNowExists()
        {
            // Arrange
            var newBook = new CreateBookCommand
            {
                Isbn = "1234567891001",
                Title = "Test-Book-2",
                PublicationDate = DateOnly.MinValue,
                GenreId = 1,
                PublisherId = 2,
                AuthorsIds = [ 1 ]
            };

            var mediator = _serviceProvider.GetService<IMediator>();
            var controller = new BooksController(mediator!);

            // Act
            await controller.CreateBook(newBook, CancellationToken.None);

            // Assert
            var dbContext = _serviceProvider.GetService<AppDbContext>();
            Assert.True(dbContext!.Books.Any(b => b.Isbn == newBook.Isbn));
        }

        [Fact]
        public async Task BooksController_DeleteBook_BookNowDoesntExist()
        {
            // Arrange
            var bookToDeleteId = 1;

            var mediator = _serviceProvider.GetService<IMediator>();
            var controller = new BooksController(mediator!);

            // Act
            await controller.DeleteBook(bookToDeleteId, CancellationToken.None);

            // Assert
            var dbContext = _serviceProvider.GetService<AppDbContext>();
            Assert.False(dbContext!.Books.Any(b => b.Id == bookToDeleteId));
        }

        [Fact]
        public async Task BooksController_UpdateBook_BookNowChanged()
        {
            // Arrange
            var bookToChangeId = 1;
            var newBook = new UpdateBookCommand
            {
                Isbn = "1234567891001",
                Title = "Test-Book-2",
                PublicationDate = DateOnly.MinValue,
                GenreId = 1,
                PublisherId = 2,
                AuthorsIds = [1]
            };

            var mediator = _serviceProvider.GetService<IMediator>();
            var controller = new BooksController(mediator!);

            // Act
            await controller.UpdateBook(bookToChangeId, newBook, CancellationToken.None);

            // Assert
            var dbContext = _serviceProvider.GetService<AppDbContext>();
            var changedBook = dbContext!.Books.FirstOrDefault(b => b.Id == bookToChangeId);
            Assert.NotNull(changedBook);
            Assert.True(changedBook.Isbn == newBook.Isbn);
        }

        private void SeedContext()
        {
            var dbContext = _serviceProvider.GetService<AppDbContext>();

            dbContext!.Genres.Add(new Genre { Id = 1, Name = "Test-Genre-1" });

            dbContext.Publishers.AddRange(
                new Publisher { Id = 1, Name = "Test-Publisher-1" },
                new Publisher { Id = 2, Name = "Test-Publisher-2" });

            dbContext.Authors.AddRange(
                new Author { Id = 1, FirstName = "John", LastName = "Doe" },
                new Author { Id = 2, FirstName = "Adam", LastName = "Smith" });

            dbContext.Books.AddRange(
                new Book
                {
                    Id = 1,
                    Isbn = "1234567891000",
                    Title = "Test-Book-1",
                    PublicationDate = DateOnly.MinValue,
                    Approved = false,
                    GenreId = 1,
                    PublisherId = 1
                });

            dbContext.SaveChanges();
        }
    }
}