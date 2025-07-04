
using BookBarter.Domain.Entities;
using CsvHelper;
using System.Globalization;
using System.Reflection;
using CsvHelper.Configuration.Attributes;

namespace BookBarter.Infrastructure.DataSeed;

public class GenreSeed
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Genres.Any()) return;

        var dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataSeed/Data/genres.csv";
        using var reader = new StreamReader(dataPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<GenreCsvMap>()
            .Select(r => new Genre
            {
                Name = r.Name,
            })
            .ToList();

        context.Genres.AddRange(records);
        await context.SaveChangesAsync();
    }
}

public class GenreCsvMap
{
    [Name("name")] public string Name { get; set; } = default!;
}
