using BookBarter.Domain.Entities;
using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration.Attributes;
using System.Reflection;

namespace BookBarter.Infrastructure.DataSeed;

public class CitiesSeed
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Cities.Any()) return;

        var dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataSeed/Data/cities.csv";
        using var reader = new StreamReader(dataPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<CityCsvMap>()
            .Select(r => new City
            {
                Name = r.City,
                CountryName = r.Country
            })
            .ToList();

        context.Cities.AddRange(records);
        await context.SaveChangesAsync();
    }
}

public class CityCsvMap
{
    [Name("city")] public string City { get; set; } = default!;
    [Name("country")] public string Country { get; set; } = default!;
}
