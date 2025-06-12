
using BookBarter.Application.Common.Interfaces.Auth;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Infrastructure.Auth;
using BookBarter.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IGenericRepository), typeof(GenericRepository));
        services.AddScoped(typeof(IBookRepository), typeof(BookRepository));

        services.AddTransient<IAuthenticationService, AuthenticationService>();
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}
