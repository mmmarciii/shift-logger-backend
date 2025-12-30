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
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();