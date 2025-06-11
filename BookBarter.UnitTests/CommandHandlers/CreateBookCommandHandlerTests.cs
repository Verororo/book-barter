using BookBarter.Application.Books.Commands;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using Moq;
using System.Linq.Expressions;

namespace BookBarter.UnitTests.CommandHandlers;

public class CreateBookCommandHandlerTests
{
    private readonly Mock<IGenericRepository> _repositoryMock;
    private readonly Mock<IEntityExistenceValidator> _validatorMock;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _repositoryMock = new Mock<IGenericRepository>();
        _validatorMock = new Mock<IEntityExistenceValidator>();
        _handler = new CreateBookCommandHandler(
            _repositoryMock.Object,
            _validatorMock.Object
        );
    }

    [Fact]
    public async Task CreateBook_ValidData_CreatesAndSavesBook()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Isbn = "1234567891000",
            Title = "Test Book",
            PublicationDate = DateOnly.MinValue,
            AuthorsIds = new List<int> { 1, 2 },
            GenreId = 1,
            PublisherId = 1
        };

        var authors = new List<Author>
        {
            new Author { Id = 1 },
            new Author { Id = 2 }
        };

        _validatorMock.Setup(v => v.ValidateAsync<Genre>(command.GenreId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _validatorMock.Setup(v => v.ValidateAsync<Publisher>(command.PublisherId, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
        _validatorMock.Setup(v => v.ValidateAsync<Author>(command.AuthorsIds, It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _repositoryMock.Setup(r => r.GetByPredicateAsync<Author>(
                It.IsAny<Expression<Func<Author, bool>>>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(authors);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _validatorMock.Verify(v => v.ValidateAsync<Genre>(command.GenreId, 
            It.IsAny<CancellationToken>()), Times.Once);
        _validatorMock.Verify(v => v.ValidateAsync<Publisher>(command.PublisherId, 
            It.IsAny<CancellationToken>()), Times.Once);
        _validatorMock.Verify(v => v.ValidateAsync<Author>(command.AuthorsIds, 
            It.IsAny<CancellationToken>()), Times.Once);

        _repositoryMock.Verify(r => r.Add(It.Is<Book>(b =>
            b.Isbn == command.Isbn &&
            b.Title == command.Title &&
            b.PublicationDate == command.PublicationDate &&
            b.GenreId == command.GenreId &&
            b.PublisherId == command.PublisherId &&
            b.Authors.SequenceEqual(authors)
        )), Times.Once);

        _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task CreateBook_InvalidGenre_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            GenreId = 0,
            AuthorsIds = new List<int> { 1 },
            PublisherId = 1
        };

        _validatorMock.Setup(v => v.ValidateAsync<Genre>(command.GenreId, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Genre), command.GenreId));

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, 
            CancellationToken.None));
    }

    [Fact]
    public async Task CreateBook_InvalidPublisher_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            GenreId = 1,
            AuthorsIds = new List<int> { 1 },
            PublisherId = 0
        };

        _validatorMock.Setup(v => v.ValidateAsync<Publisher>(command.PublisherId, 
            It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Publisher), command.PublisherId));

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command,
            CancellationToken.None));
    }

    [Fact]
    public async Task CreateBook_InvalidAuthors_ThrowsEntityNotFoundException()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            GenreId = 1,
            AuthorsIds = new List<int> { 0 },
            PublisherId = 1
        };

        _validatorMock.Setup(v => v.ValidateAsync<Author>(command.AuthorsIds, It.IsAny<CancellationToken>()))
            .ThrowsAsync(new EntityNotFoundException(nameof(Author), command.AuthorsIds.First()));

        // Act & Assert
        await Assert.ThrowsAsync<EntityNotFoundException>(() => _handler.Handle(command, 
            CancellationToken.None));
    }
}