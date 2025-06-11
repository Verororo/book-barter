
using AutoMapper;
using BookBarter.Application.Books.Queries;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Common.Interfaces.Repositories;
using BookBarter.Application.Common.Services;
using BookBarter.Infrastructure.Repositories;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BookBarter.IntegrationTests.Helpers;

public static class TestHelpers
{
    public static IMapper CreateMapper()
    {
        var services = new ServiceCollection();
        var assembly = typeof(GetByIdBookQuery).Assembly;
        services.AddAutoMapper(assembly);
        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMapper>();
    }

    public static IMediator CreateMediator()
    {
        var services = new ServiceCollection();
        var assembly = typeof(GetByIdBookQuery).Assembly;

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddScoped<IEntityExistenceValidator, EntityExistenceValidator>();
        services.AddScoped<IBookRepository, BookRepository>();

        var serviceProvider = services.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMediator>();
    }
}
