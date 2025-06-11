using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Books.Responses;
using BookBarter.Domain.Exceptions;
using Moq;

namespace BookBarter.UnitTests.QueryHandlers
{
    public class GetByIdBookTests
    {
        private readonly Mock<IBookRepository> _bookRepositoryMock;
        private readonly GetByIdBookQueryHandler _handler;

        public GetByIdBookTests()
        {
            _bookRepositoryMock = new Mock<IBookRepository>();
            _handler = new GetByIdBookQueryHandler(_bookRepositoryMock.Object);
        }

        [Fact]
        public async Task GetByIdBook_ValidId_ReturnsBookDto()
        {
            // Arrange
            var validId = 1;
            var expectedResult = new BookDto { Id = validId, Title = "Test Book" };

            _bookRepositoryMock.Setup(r => r.GetDtoByIdAsync(validId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResult);

            var query = new GetByIdBookQuery { Id = validId };

            // Act
            var actualResult = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(actualResult);
            Assert.Equal(expectedResult.Id, actualResult.Id);
            Assert.Equal(expectedResult.Title, actualResult.Title);

            _bookRepositoryMock.Verify(r => r.GetDtoByIdAsync(validId, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetByIdBook_InvalidId_ThrowsEntityNotFoundException()
        {
            // Arrange
            var invalidId = 0;
            _bookRepositoryMock.Setup(r => r.GetDtoByIdAsync(invalidId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((BookDto?)null);

            var query = new GetByIdBookQuery { Id = invalidId };

            // Act & Assert
            var exception = await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _handler.Handle(query, CancellationToken.None));

            _bookRepositoryMock.Verify(r => r.GetDtoByIdAsync(invalidId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}