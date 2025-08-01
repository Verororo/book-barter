﻿using BookBarter.API.Common.Services;
using BookBarter.API.Hubs.UserIdProvider;
using BookBarter.Application.Common;
using BookBarter.Application.Common.Interfaces;
using BookBarter.Application.Extensions;
using BookBarter.Infrastructure;
using BookBarter.Infrastructure.Extensions;
using BookBarter.Infrastructure.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

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
        builder.Services.AddAuthentication(builder.Configuration);

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new DateTimeConverter());
            });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddAutoMapper(typeof(CommonProfile).Assembly);
        builder.Services.AddSwagger();

        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        builder.Services.AddSingleton<IUserIdProvider, UserIdProvider>();

        builder.Services.AddCors(opts =>
            opts.AddPolicy("DevPolicy", builder =>
            builder.WithOrigins("http://localhost:5173")
           .AllowAnyMethod()
           .AllowAnyHeader()
           .AllowCredentials()
            )
        );
        builder.Services.AddSignalR(hubOptions =>
        {
            hubOptions.EnableDetailedErrors = true;
        });
    }

    public static IServiceCollection AddAuthentication(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var authOptions = services.ConfigureAuthOptions(configuration);
        services.AddJwtAuthentication(authOptions);
        return services;
    }

    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Description = "Please enter token",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = JwtBearerDefaults.AuthenticationScheme,
                                Type = ReferenceType.SecurityScheme,
                            }
                        },
                        new string[] { }
                }
            });
        });
        return services;
    }

    private static AuthOptions ConfigureAuthOptions(this IServiceCollection services, 
        IConfiguration configuration)
    {
        var authOptionsConfigurationSection = configuration.GetSection("AuthOptions");
        services.Configure<AuthOptions>(authOptionsConfigurationSection);
        var authOptions = authOptionsConfigurationSection.Get<AuthOptions>();
        return authOptions!;
    }

    private static void AddJwtAuthentication(this IServiceCollection services, AuthOptions authOptions)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = authOptions.Issuer,

                ValidateAudience = true,
                ValidAudience = authOptions.Audience,

                ValidateLifetime = true,

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
            };

            // Support SignalR authentication
            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/messageHub"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }
}
