using GameLibrary.Domain;
using GameLibrary.Integration.Config;
using GameLibrary.Repository;
using GameLibrary.Service;
using GameLibrary.Service.Services;
using GameLibrary.Service.Services.Interface;
using GameLibrary.Integration.Handlers;

var builder = WebApplication.CreateBuilder(args);

// Initialize AppConfig singleton
AppConfig.Instance.Initialize(builder.Configuration);

// Add global exception handler and problem details
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

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
builder.Services.AddHttpClient<IFreeToGameService, FreeToGameService>(client =>
{
    var baseUrl = AppConfig.Instance.FreeToGameApiBaseUrl;
    if (string.IsNullOrWhiteSpace(baseUrl))
        throw new InvalidOperationException("FreeToGameApiBaseUrl is not configured.");
    client.BaseAddress = new Uri(baseUrl);
}
);

var app = builder.Build();

app.UseExceptionHandler();

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
