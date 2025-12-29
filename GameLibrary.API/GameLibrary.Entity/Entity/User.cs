using GameLibrary.Integration.Logger;

namespace GameLibrary.Entity.Entities;

public class User : BaseEntity
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? PasswordHash { get; set; }
    
    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public User() { }

    public User(string username, string email)
    {
        if (string.IsNullOrEmpty(username))
        {
            LoggerSingleton.GetInstance().Log("Invalid username (empty).", LogLevel.Error);
        }
        if (string.IsNullOrEmpty(email))
        {
            LoggerSingleton.GetInstance().Log("Invalid email (empty).", LogLevel.Error);
        }
        
        Username = username;
        Email = email;
    }

    public void SetUsername(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty username.", LogLevel.Error);
            return;
        }
        Username = username;
    }

    public void SetEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty email.", LogLevel.Error);
            return;
        }
        Email = email;
    }

    public override void PrintInformation()
    {
        Console.WriteLine($"User: {Id}, {Username}, {Email}");
    }
}
