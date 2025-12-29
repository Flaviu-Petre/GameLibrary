using GameLibrary.Integration.Logger;

namespace GameLibrary.Entity.Entities;

public class Publisher : BaseEntity
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public string? Website { get; set; }
    
    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public Publisher() { }

    public Publisher(string name, string country)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Invalid publisher name (empty).", LogLevel.Error);
        }
        
        Name = name;
        Country = country;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty publisher name.", LogLevel.Error);
            return;
        }
        Name = name;
    }

    public override void PrintInformation()
    {
        Console.WriteLine($"Publisher: {Id}, {Name}, {Country}");
    }
}
