using BookBarter.Application.Books.Commands;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using Moq;
using System.Linq.Expressions;

namespace BookBarter.UnitTests.CommandHandlers;

public class UpdateBookCommandHandlerTests
{
    private readonly Mock<IGenericRepository> _repositoryMock;
    private readonly Mock<IEntityExistenceValidator> _validatorMock;
    private readonly UpdateBookCommandHandler _handler;

    public UpdateBookCommandHandlerTests()
    {
        _repositoryMock = new Mock<IGenericRepository>();
        _validatorMock = new Mock<IEntityExistenceValidator>();
        _handler = new UpdateBookCommandHandler(
            _repositoryMock.Object,
            _validatorMock.Object
        );
    }

    [Fact]
    public async Task Handle_ValidData_UpdatesBook()
    {
        // Arrange
        const int bookId = 1;
        var command = new UpdateBookCommand
        {
            Id = bookId,
            Isbn = "1234567891001",
            Title = "New Title",
            PublicationDate = new DateOnly(2023, 6, 15),
            AuthorsIds = new List<int> { 2, 3 },
            GenreId = 2,
            PublisherId = 2
        };

        var existingBook = new Book
        {
            Id = bookId,
            Isbn = "1234567891000",
            Title = "Old Title",
            Authors = new List<Author> { new Author { Id = 1 } },
            PublicationDate = DateOnly.MinValue
        };

        var newAuthors = new List<Author>
        {
            new Author { Id = 2 },
            new Author { Id = 3 }
        };

        _validatorMock.Setup(v => v.ValidateAsync<Genre>(command.GenreId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _validatorMock.Setup(v => v.ValidateAsync<Publisher>(command.PublisherId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _validatorMock.Setup(v => v.ValidateAsync<Author>(command.AuthorsIds, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _repositoryMock.Setup(r => r.GetByIdAsync<Book>(bookId, It.IsAny<CancellationToken>(), 
            It.IsAny<Expression<Func<Book, object>>[]>()))
            .ReturnsAsync(existingBook);

        _repositoryMock.Setup(r => r.GetByPredicateAsync(
                It.IsAny<Expression<Func<Author, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(newAuthors);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _validatorMock.Verify(v => v.ValidateAsync<Genre>(command.GenreId, It.IsAny<CancellationToken>()), 
            Times.Once);
        _validatorMock.Verify(v => v.ValidateAsync<Publisher>(command.PublisherId, It.IsAny<CancellationToken>()), 
            Times.Once);
        _validatorMock.Verify(v => v.ValidateAsync<Author>(command.AuthorsIds, It.IsAny<CancellationToken>()), 
            Times.Once);

        _repositoryMock.Verify(r => r.GetByIdAsync<Book>(
            bookId,
            It.IsAny<CancellationToken>(),
            It.IsAny<Expression<Func<Book, object>>[]>()),
            Times.Once);

        _repositoryMock.Verify(r => r.GetByPredicateAsync(
            It.IsAny<Expression<Func<Author, bool>>>(),
            It.IsAny<CancellationToken>()),
            Times.Once);

        Assert.Equal(command.Isbn, existingBook.Isbn);
        Assert.Equal(command.Title, existingBook.Title);
        Assert.Equal(command.PublicationDate, existingBook.PublicationDate);
        Assert.Equal(command.GenreId, existingBook.GenreId);
        Assert.Equal(command.PublisherId, existingBook.PublisherId);
        Assert.Equal(newAuthors, existingBook.Authors);

        _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidBookId_ThrowsEntityNotFoundException()
    {
        // Arrange
        const int invalidId = 0;
        var command = new UpdateBookCommand { Id = invalidId };

        _repositoryMock.Setup(r => r.GetByIdAsync<Book>(invalidId, It.IsAny<CancellationToken>(), 
            It.IsAny<Expression<Func<Book, object>>[]>()))
            .ReturnsAsync((Book?)null);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidGenre_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new UpdateBookCommand
        {
            Id = 1,
            GenreId = 0,
            AuthorsIds = new List<int> { 1 }
        };

        _validatorMock.Setup(v => v.ValidateAsync<Genre>(command.GenreId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Genre), command.GenreId));

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task UpdateBook_InvalidPublisher_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new UpdateBookCommand
        {
            GenreId = 1,
            AuthorsIds = new List<int> { 1 },
            PublisherId = 0
        };

        _validatorMock.Setup(v => v.ValidateAsync<Publisher>(command.PublisherId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Publisher), command.PublisherId));

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_InvalidAuthors_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new UpdateBookCommand
        {
            Id = 1,
            GenreId = 1,
            AuthorsIds = new List<int> { 0 }
        };

        _validatorMock.Setup(v => v.ValidateAsync<Author>(command.AuthorsIds, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Author), command.AuthorsIds.First()));

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    private void SetupSuccessfulValidationMocks()
    {
        _validatorMock.Setup(v => v.ValidateAsync<Genre>(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _validatorMock.Setup(v => v.ValidateAsync<Publisher>(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _validatorMock.Setup(v => v.ValidateAsync<Author>(It.IsAny<List<int>>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
    }
}