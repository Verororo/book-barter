
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;

namespace BookBarter.Application.Books.Commands;
public record CreateBookCommand(
    string isbn, 
    string title, 
    DateOnly publicationDate, 
    int genreId,
    ICollection<Author> authors,
    string? newGenreName = null) : IRequest; // DTO

// CreateBookCommandHandler
// TODO: remove GetAll and GetPredicate business logic operations and replace them with more specific ones
public class CreateBookHandler : IRequestHandler<CreateBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    public CreateBookHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = new Book
        {
            Isbn = request.isbn,
            Title = request.title,
            PublicationDate = request.publicationDate,
            Authors = request.authors
        };

        if (request.genreId != 0)
        {
            book.GenreId = request.genreId;
        }
        else
        {
            if (request.newGenreName == null)
            {
                throw new Exception($"Genre id was specified as 0, but no new genre name was provided");
            }
            book.Genre = new Genre { Name = request.newGenreName };
        }

        _bookRepository.Add(book);
        await _bookRepository.SaveAsync();
    }
}
