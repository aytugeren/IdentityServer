using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Movies.API.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MoviesAPIContext>(options =>
    options.UseInMemoryDatabase("Movies"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

SeedDatabase(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void SeedDatabase(WebApplication builder)
{
    using (var scope = builder.Services.CreateScope())
    {
        var service = scope.ServiceProvider;
        var moviesContext = service.GetRequiredService<MoviesAPIContext>();
        MoviesContextSeed.SeedAsync(moviesContext);
    }
}