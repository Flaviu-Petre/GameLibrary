using GameLibrary.Integration.Logger;

namespace GameLibrary.Entity.Entities;

public class Developer : BaseEntity
{
    public string? Name { get; set; }
    public string? Country { get; set; }
    public DateTime? FoundedDate { get; set; }
    
    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public Developer() { }

    public Developer(string name, string country)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Invalid developer name (empty).", LogLevel.Error);
        }
        
        Name = name;
        Country = country;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty developer name.", LogLevel.Error);
            return;
        }
        Name = name;
    }

    public void SetCountry(string country)
    {
        Country = country;
    }

    public override void PrintInformation()
    {
        Console.WriteLine($"Developer: {Id}, {Name}, {Country}");
    }
}
