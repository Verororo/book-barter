using BookBarter.Application.BookBooks.Commands;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Domain.Entities;
using BookBarter.Domain.Exceptions;
using Moq;

namespace BookBarter.UnitTests.CommandHandlers;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IGenericRepository> _repositoryMock;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _repositoryMock = new Mock<IGenericRepository>();
        _handler = new DeleteBookCommandHandler(_repositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ValidId_DeletesBookAndSavesChanges()
    {
        // Arrange
        var existingBook = new Book 
        { 
            Id = 1,
            Isbn = "1234567891000",
            Title = "Test Book",
            PublicationDate = DateOnly.MinValue
        };

        _repositoryMock.Setup(r => r.GetByIdAsync<Book>(existingBook.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(existingBook);

        var command = new DeleteBookCommand { Id = existingBook.Id };

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _repositoryMock.Verify(r => r.GetByIdAsync<Book>(existingBook.Id, It.IsAny<CancellationToken>()), 
            Times.Once);
        _repositoryMock.Verify(r => r.Delete<Book>(existingBook), Times.Once);
        _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_InvalidId_ThrowsEntityNotFoundException()
    {
        // Arrange
        int invalidId = 0;

        _repositoryMock.Setup(r => r.GetByIdAsync<Book>(invalidId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Book?)null);

        var command = new DeleteBookCommand { Id = invalidId };

        // Act & Assert
        var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
            _handler.Handle(command, CancellationToken.None));

        _repositoryMock.Verify(r => r.GetByIdAsync<Book>(invalidId, It.IsAny<CancellationToken>()), 
            Times.Once);
        _repositoryMock.Verify(r => r.Delete<Book>(It.IsAny<Book>()), Times.Never);
        _repositoryMock.Verify(r => r.SaveAsync(It.IsAny<CancellationToken>()), Times.Never);
    }
}