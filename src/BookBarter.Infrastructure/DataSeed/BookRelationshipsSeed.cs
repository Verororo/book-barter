using BookBarter.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookBarter.Infrastructure.DataSeed;

public class BookRelationshipsSeed
{
    public static async Task Seed(UserManager<User> userManager, AppDbContext context)
    {
        if (context.OwnedBooks.Any() || context.WantedBooks.Any()) return;

        // Add books to Alexei
        var alexei = context.Users.First(u => u.UserName == "Alexei");
        await context.AddRangeAsync([
            new OwnedBook {
                User = alexei,
                Book = context.Books.First(b => b.Title == "War and Peace"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            },
            new OwnedBook {
                User = alexei,
                Book = context.Books.First(b => b.Title == "Tractatus Logico-Philosophicus"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = alexei,
                Book = context.Books.First(b => b.Title == "Les Miserables"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Anna
        var anna = context.Users.First(u => u.UserName == "Anna");
        await context.AddRangeAsync([
            new OwnedBook {
                User = anna,
                Book = context.Books.First(b => b.Title == "The Fellowship of The Ring"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = anna,
                Book = context.Books.First(b => b.Title == "War and Peace"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Vasily
        var vasily = context.Users.First(u => u.UserName == "Vasily");
        await context.AddRangeAsync([
            new OwnedBook {
                User = vasily,
                Book = context.Books.First(b => b.Title == "Atlas Shrugged"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = vasily,
                Book = context.Books.First(b => b.Title == "Les Miserables"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Alexandr
        var alexandr = context.Users.First(u => u.UserName == "Alexandr");
        await context.AddRangeAsync([
            new OwnedBook {
                User = alexandr,
                Book = context.Books.First(b => b.Title == "1984"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            },
            new OwnedBook {
                User = alexandr,
                Book = context.Books.First(b => b.Title == "The Stranger"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = alexandr,
                Book = context.Books.First(b => b.Title == "To Kill a Mockingbird"),
                AddedDate = DateTime.Now
            },
            new WantedBook {
                User = alexandr,
                Book = context.Books.First(b => b.Title == "Hamlet"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Maria
        var maria = context.Users.First(u => u.UserName == "Maria");
        await context.AddRangeAsync([
            new OwnedBook {
                User = maria,
                Book = context.Books.First(b => b.Title == "Pride and Prejudice"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = maria,
                Book = context.Books.First(b => b.Title == "To the Lighthouse"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Oleg
        var oleg = context.Users.First(u => u.UserName == "Oleg");
        await context.AddRangeAsync([
            new OwnedBook {
                User = oleg,
                Book = context.Books.First(b => b.Title == "One Hundred Years of Solitude"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            },
            new OwnedBook {
                User = oleg,
                Book = context.Books.First(b => b.Title == "Introduction to Algorithms"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = oleg,
                Book = context.Books.First(b => b.Title == "Structure and Implementation of Computer Programs"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Nikolai
        var nikolai = context.Users.First(u => u.UserName == "Nikolai");
        await context.AddRangeAsync([
            new OwnedBook {
                User = nikolai,
                Book = context.Books.First(b => b.Title == "The Metamorphosis"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = nikolai,
                Book = context.Books.First(b => b.Title == "The Idiot"),
                AddedDate = DateTime.Now
            },
            new WantedBook {
                User = nikolai,
                Book = context.Books.First(b => b.Title == "Blood Meridian"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Arina
        var arina = context.Users.First(u => u.UserName == "Arina");
        await context.AddRangeAsync([
            new OwnedBook {
                User = arina,
                Book = context.Books.First(b => b.Title == "Ulysses"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = arina,
                Book = context.Books.First(b => b.Title == "Paradise Lost"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Sofia
        var sofia = context.Users.First(u => u.UserName == "Sofia");
        await context.AddRangeAsync([
            new OwnedBook {
        User = sofia,
        Book = context.Books.First(b => b.Title == "Great Expectations"),
        BookStateId = 3,
        AddedDate = DateTime.Now
        },
        new OwnedBook {
                User = sofia,
                Book = context.Books.First(b => b.Title == "Understanding Analysis"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = sofia,
                Book = context.Books.First(b => b.Title == "Economics Rules"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Mihai
        var mihai = context.Users.First(u => u.UserName == "Mihai");
        await context.AddRangeAsync([
            new OwnedBook {
                User = mihai,
                Book = context.Books.First(b => b.Title == "Moby Dick"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = mihai,
                Book = context.Books.First(b => b.Title == "Plato: Complete Works"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Jan
        var jan = context.Users.First(u => u.UserName == "Jan");
        await context.AddRangeAsync([
            new OwnedBook {
                User = jan,
                Book = context.Books.First(b => b.Title == "Concrete Mathematics"),
                BookStateId = 2,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = jan,
                Book = context.Books.First(b => b.Title == "The Art of Computer Programming, Volumes 1-4"),
                AddedDate = DateTime.Now
            }
        ]);

        // Add books to Denis
        var denis = context.Users.First(u => u.UserName == "Denis");
        await context.AddRangeAsync([
            new OwnedBook {
                User = denis,
                Book = context.Books.First(b => b.Title == "To Kill a Mockingbird"),
                BookStateId = 3,
                AddedDate = DateTime.Now
            }
        ]);
        await context.AddRangeAsync([
            new WantedBook {
                User = denis,
                Book = context.Books.First(b => b.Title == "1984"),
                AddedDate = DateTime.Now
            }
        ]);

        await context.SaveChangesAsync();
    }
}
