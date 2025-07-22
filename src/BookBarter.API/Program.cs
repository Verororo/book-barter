using BookBarter.API.Extensions;
using BookBarter.API.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.AddServices();
//builder.Services.Configure<RequestLoggingOptions>(options => options.SensitiveHeaders = [ ... ]

var app = builder.Build();

await app.SeedDataApiAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("DevPolicy");
app.UseRequestLogging();
app.UseExceptionHandling();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseTransaction();

app.MapControllers()
   .RequireAuthorization();

app.MapHub<MessageHub>("/messageHub");

app.Run();
