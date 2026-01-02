using GameLibrary.Integration.Config.Models;
using Microsoft.Extensions.Configuration;

namespace GameLibrary.Integration.Config;

public class AppConfig
{
    private static AppConfig? _instance;
    private static readonly object _lock = new();
    
    public ConnectionStringsSettings? ConnectionStrings { get; private set; }
    public string? FreeToGameApiBaseUrl { get; private set; }
    
    private AppConfig() { }
    
    public static AppConfig Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new AppConfig();
                }
            }
            return _instance;
        }
    }
    
    public void Initialize(IConfiguration configuration)
    {
        ConnectionStrings = configuration.GetSection("ConnectionStrings")
            .Get<ConnectionStringsSettings>();
        FreeToGameApiBaseUrl = configuration.GetValue<string>("FreeToGameApiBaseUrl");
    }
    
    public string? GetConnectionString(string name = "DefaultConnection")
    {
        return name switch
        {
            "DefaultConnection" => ConnectionStrings?.DefaultConnection,
            _ => null
        };
    }
}
