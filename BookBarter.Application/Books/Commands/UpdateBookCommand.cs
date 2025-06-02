
using BookBarter.Application.Abstractions;
using BookBarter.Domain.Entities;
using MediatR;
using BookBarter.Domain.Exceptions;

namespace BookBarter.Application.Books.Commands;
public record UpdateBookCommand(
    int id, 
    string isbn, 
    string title, 
    DateOnly publicationDate, 
    int genreId,
    ICollection<Author> authors,
    string? newGenreName = null) : IRequest;
public class UpdateBookHandler : IRequestHandler<UpdateBookCommand>
{
    private readonly IRepository<Book> _bookRepository;
    public UpdateBookHandler(IRepository<Book> bookRepository)
    {
        _bookRepository = bookRepository;
    }
    public async Task Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.id);
        if (book == null)
        {
            throw new EntityNotFoundException($"Book with id {request.id} has not been found");
        }

        book.Id = request.id;
        book.Isbn = request.isbn;
        book.Title = request.title;
        book.PublicationDate = request.publicationDate;
        book.Authors = request.authors;

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

        await _bookRepository.SaveAsync();
    }
}
