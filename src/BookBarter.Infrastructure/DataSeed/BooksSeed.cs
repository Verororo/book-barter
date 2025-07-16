using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using CsvHelper.Configuration.Attributes;
using CsvHelper;

namespace BookBarter.Infrastructure.DataSeed;

public class BooksSeed
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Books.Any()) return;

        List<Book> records = [
            new Book
            {
                Isbn = "9781400079988",
                Title = "War and Peace",
                PublicationDate = DateOnly.Parse("2008-12-02"),
                AddedDate = DateTime.UtcNow,
                Approved = true,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Vintage"),
                Authors = [context.Authors.First(a => a.LastName == "Tolstoy")]
            },

            new Book
            {
                Isbn = "9781613820254",
                Title = "Les Miserables",
                PublicationDate = DateOnly.Parse("2012-09-26"),
                AddedDate = DateTime.UtcNow,
                Approved = true,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Simon & Brown"),
                Authors = [context.Authors.First(a => a.LastName == "Hugo")]
            },

            new Book
            {
                Isbn = "9781604240535",
                Title = "Tractatus Logico-Philosophicus",
                PublicationDate = DateOnly.Parse("2007-09-06"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Philosophy"),
                Publisher = context.Publishers.First(p => p.Name == "Book Jungle"),
                Authors = [context.Authors.First(a => a.LastName == "Wittgenstein")]
            },

            new Book
            {
                Isbn = "9780345339706",
                Title = "The Fellowship of the Ring",
                PublicationDate = DateOnly.Parse("1986-08-12"),
                AddedDate = DateTime.UtcNow,
                Approved = true,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Del Rey"),
                Authors = [context.Authors.First(a => a.LastName == "Tolkien")]
            },

            new Book
            {
                Isbn = "9780262510875",
                Title = "Structure and Implementation of Computer Programs",
                PublicationDate = DateOnly.Parse("1996-09-01"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Textbook"),
                Publisher = context.Publishers.First(p => p.Name == "The MIT Press"),
                Authors = [context.Authors.First(a => a.LastName == "Sussman"),
                           context.Authors.First(a => a.LastName == "Abelson")]
            },

            new Book
            {
                Isbn = "9780451191144",
                Title = "Atlas Shrugged",
                PublicationDate = DateOnly.Parse("1996-09-01"),
                AddedDate = DateTime.UtcNow,
                Approved = true,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Signet"),
                Authors = [context.Authors.First(a => a.LastName == "Rand")]
            },

            new Book
            {
                Isbn = "9781493927111",
                Title = "Understanding Analysis",
                PublicationDate = DateOnly.Parse("2015-05-20"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Textbook"),
                Publisher = context.Publishers.First(p => p.Name == "Springer"),
                Authors = [context.Authors.First(a => a.LastName == "Abbott")]
            },

            new Book
            {
                Isbn = "9780262046305",
                Title = "Introduction to Algorithms",
                PublicationDate = DateOnly.Parse("2022-04-05"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Textbook"),
                Publisher = context.Publishers.First(p => p.Name == "The MIT Press"),
                Authors = [context.Authors.First(a => a.LastName == "Cormen")]
            },

            new Book
            {
                Isbn = "9780321751041",
                Title = "The Art of Computer Programming, Volumes 1-4",
                PublicationDate = DateOnly.Parse("2011-03-03"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Textbook"),
                Publisher = context.Publishers.First(p => p.Name == "Addison-Wesley Professional"),
                Authors = [context.Authors.First(a => a.LastName == "Knuth")]
            },

            new Book
            {
                Isbn = "9780201558029",
                Title = "Concrete Mathematics",
                PublicationDate = DateOnly.Parse("1994-02-28"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Textbook"),
                Publisher = context.Publishers.First(p => p.Name == "Addison-Wesley Professional"),
                Authors = [context.Authors.First(a => a.LastName == "Knuth"),
                           context.Authors.First(a => a.LastName == "Patashnik"),
                           context.Authors.First(a => a.LastName == "Graham")]
            },

            new Book
            {
                Isbn = "9780140447927",
                Title = "The Idiot",
                PublicationDate = DateOnly.Parse("2004-08-31"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Penguin Classics"),
                Authors = [context.Authors.First(a => a.LastName == "Dostoevsky")]
            },

            new Book
            {
                Isbn = "9780143105954",
                Title = "Moby Dick",
                PublicationDate = DateOnly.Parse("2009-10-27"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Penguin Classics"),
                Authors = [context.Authors.First(a => a.LastName == "Melville")]
            },

            new Book
            {
                Isbn = "9780140424393",
                Title = "Paradise Lost",
                PublicationDate = DateOnly.Parse("2003-04-29"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Poetry"),
                Publisher = context.Publishers.First(p => p.Name == "Penguin Classics"),
                Authors = [context.Authors.First(a => a.LastName == "Milton")]
            },

            new Book
            {
                Isbn = "9780679728757",
                Title = "Blood Meridian",
                PublicationDate = DateOnly.Parse("1992-05-05"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Novel"),
                Publisher = context.Publishers.First(p => p.Name == "Vintage"),
                Authors = [context.Authors.First(a => a.LastName == "McCarthy")]
            },

            new Book
            {
                Isbn = "9780393246414",
                Title = "Economics Rules",
                PublicationDate = DateOnly.Parse("2015-10-13"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Popular Science"),
                Publisher = context.Publishers.First(p => p.Name == "W. W. Norton & Company"),
                Authors = [context.Authors.First(a => a.LastName == "Rodrik")]
            },

            new Book
            {
                Isbn = "9780872203495",
                Title = "Plato: Complete Works",
                PublicationDate = DateOnly.Parse("1997-05-01"),
                AddedDate = DateTime.UtcNow,
                Genre = context.Genres.First(g => g.Name == "Philosophy"),
                Publisher = context.Publishers.First(p => p.Name == "Hackett Publishing Co."),
                Authors = [context.Authors.First(a => a.LastName == "Plato"),
                           context.Authors.First(a => a.LastName == "Cooper"),
                           context.Authors.First(a => a.LastName == "Hutchinson")]
            }
        ];

        context.Books.AddRange(records);
        await context.SaveChangesAsync();
    }
}