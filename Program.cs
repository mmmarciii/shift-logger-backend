using Backend.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Database - SQLite path
var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "shifts.db");
builder.Services.AddDbContext<ShiftsContext>(options =>
    options.UseSqlite($"Data Source={dbPath}"));

builder.Services.AddControllers();

// (Microsoft.AspNetCore.OpenApi)
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular",
        policy =>
        {
            policy.WithOrigins("http://localhost:4200", "https://shiftslogger.netlify.app/")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors("AllowAngular");
app.MapControllers();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ShiftsContext>();
        context.Database.EnsureCreated();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Hiba történt az adatbázis létrehozásakor.");
    }
}

app.Run();