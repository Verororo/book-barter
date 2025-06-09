using BookBarter.API.Extensions;
using Microsoft.Extensions.Options;
using BookBarter.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddServices();
//builder.Services.Configure<RequestLoggingOptions>(options => options.SensitiveHeaders = [ ... ]

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRequestLogging();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();
