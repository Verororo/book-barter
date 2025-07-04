using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using BookBarter.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration.Attributes;

namespace BookBarter.Infrastructure.DataSeed;

public class AuthorsSeed
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Authors.Any()) return;

        var dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataSeed/Data/authors.csv";
        using var reader = new StreamReader(dataPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<AuthorCsvMap>()
            .Select(r => new Author
            {
                FirstName = r.FirstName,
                MiddleName = r.MiddleName,
                LastName = r.LastName
            })
            .ToList();

        context.Authors.AddRange(records);
        await context.SaveChangesAsync();
    }
}

public class AuthorCsvMap
{
    [Name("firstName")] public string? FirstName { get; set; }
    [Name("middleName")] public string? MiddleName { get; set; }
    [Name("lastName")] public string LastName { get; set; } = default!;
}
