using System.Text.Json.Serialization;
using BookBarter.Application.Books;
using BookBarter.Application.Extensions;
using BookBarter.Infrastructure.Extensions;

namespace BookBarter.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddApplication();

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
                options.JsonSerializerOptions.WriteIndented = true;
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(typeof(BookProfile).Assembly);
        builder.Services.AddSwaggerGen();
    }
}
