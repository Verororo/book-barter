using BookBarter.API.Extensions;
using Microsoft.Extensions.Options;
using BookBarter.API.Middleware;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddServices();
//builder.Services.Configure<RequestLoggingOptions>(options => options.SensitiveHeaders = [ ... ]

var app = builder.Build();

await app.SeedRolesApiAsync("User", "Moderator", "Admin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLogging();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
