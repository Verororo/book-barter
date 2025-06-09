
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>((serviceProvider, options) => {
            options
                .UseSqlServer(configuration.GetConnectionString("DevelopmentServerConnection"));
        });

        services.AddScoped(typeof(IWritingRepository<>), typeof(WritingRepository<>));
        services.AddScoped(typeof(IReadingRepository<>), typeof(ReadingRepository<>));
        services.AddScoped(typeof(IBookRepository), typeof(BookRepository));

        return services;
    }
}
