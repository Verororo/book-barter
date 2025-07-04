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

public class PublisherSeed
{
    public static async Task Seed(AppDbContext context)
    {
        if (context.Publishers.Any()) return;

        var dataPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/DataSeed/Data/publishers.csv";
        using var reader = new StreamReader(dataPath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

        var records = csv.GetRecords<PublisherCsvMap>()
            .Select(r => new Publisher
            {
                Name = r.Name,
            })
            .ToList();

        context.Publishers.AddRange(records);
        await context.SaveChangesAsync();
    }
}

public class PublisherCsvMap
{
    [Name("name")] public string Name { get; set; } = default!;
}
