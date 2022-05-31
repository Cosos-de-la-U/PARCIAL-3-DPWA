using Microsoft.EntityFrameworkCore;
using PARCIAL_3_DPWA.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// CORS
builder.Services.AddCors();
// Swagger
builder.Services.AddSwaggerGen();

//DI
builder.Services.AddDbContext<railwayContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("railContext") ??
                         throw new InvalidOperationException());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CORS
app.UseCors(c => c.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
