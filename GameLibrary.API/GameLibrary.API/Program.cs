using GameLibrary.Domain;
using GameLibrary.Integration.Config;
using GameLibrary.Repository;
using GameLibrary.Service;
using Microsoft.Identity.Client;

var builder = WebApplication.CreateBuilder(args);

// Initialize AppConfig singleton
AppConfig.Instance.Initialize(builder.Configuration);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register layers using extension methods
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddRepositories(connectionString);
builder.Services.AddDomains();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
