
using BookBarter.Application.Abstractions;
using BookBarter.Application.Books.Commands;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Users.Queries;
using BookBarter.Domain.Entities;
using BookBarter.Infrastructure;
using BookBarter.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

class Program
{
    static async Task Main()
    {
        var diContainer = new ServiceCollection()
            .AddDbContext<AppDbContext>()
            .AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly))
            .AddScoped(typeof(IRepository<>), typeof(Repository<>))
            .AddScoped(typeof(IReadingRepository<>), typeof(ReadingRepository<>))
            .BuildServiceProvider();

        var mediator = diContainer.GetRequiredService<IMediator>();

        //var user1 = await mediator.Send(new CreateUser("Vasya", "vasya@example.com", "Chisinau"));
        //var user2 = await mediator.Send(new CreateUser("Anton", "anton@example.com", "Chisinau"));
        //var user3 = await mediator.Send(new CreateUser("Maxim", "maxim@example.com", "Chisinau"));

        var chisinauUsers = await mediator.Send(new GetByPredicateUsersQuery(u => u.City == "Chisinau"));
        Console.WriteLine("Users from Chisinau are:");
        foreach (var item in chisinauUsers)
        {
            Console.WriteLine(item.Name);
        }

        // adding books
        var genreClassic = new Genre { Name = "Classic" };
        var authorTolstoi = new Author { FirstName = "Leo", LastName = "Tolstoi" };
        var authorHugo = new Author { FirstName = "Victor", LastName = "Hugo" };

        //var book1 = await mediator.Send(new CreateBook("9780449300022", "Les Miserables",
        //    new DateOnly(1982, 12, 12), 1, [authorHugo]));

        var allBooks = await mediator.Send(new GetAllBooksQuery());
        Console.WriteLine("All books registered are:");
        foreach (var item in allBooks)
        {
            var authorName = item.Authors.First().FirstName + ' ' + item.Authors.First().LastName;
            Console.WriteLine(item.Title + " - " + authorName);
        }

        // adding an owned book - Les Miserables to Vasya, in an Old state
        // await mediator.Send(new CreateOwnedBook(1, 1, 1));

        var vasyasBooks = await mediator.Send(new GetAllOwnedBooksByUserQuery(1));
        Console.WriteLine("Vasya's owned books:");
        foreach (var ownedBook in vasyasBooks)
        {
            var book = ownedBook.Book;
            var authorName = book.Authors.First().FirstName + ' ' + book.Authors.First().LastName;
            Console.WriteLine(book.Title + " - " + authorName + " - " + "State: " + ownedBook.BookState.Name);
        }

        // adding a wanted book - Les Miserables to Anton
//        await mediator.Send(new CreateWantedBook(2, 1));

        var antonsBooks = await mediator.Send(new GetAllWantedBooksByUserQuery(2));
        Console.WriteLine("Antons' wanted books:");
        foreach (var book in antonsBooks)
        {
            var authorName = book.Authors.First().FirstName + ' ' + book.Authors.First().LastName;
            Console.WriteLine(book.Title + " - " + authorName);
        }
    }
}