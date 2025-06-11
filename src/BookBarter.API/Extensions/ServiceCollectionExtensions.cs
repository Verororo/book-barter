using System.Text.Json.Serialization;
using BookBarter.Application.Books;
using BookBarter.Application.Extensions;
using BookBarter.Infrastructure;
using BookBarter.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace BookBarter.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddDbContext<AppDbContext>((serviceProvider, options) => {
            options
                .UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentServerConnection"));
        });

        builder.Services.AddInfrastructure();
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
