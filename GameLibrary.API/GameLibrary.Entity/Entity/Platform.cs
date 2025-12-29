using GameLibrary.Integration.Logger;

namespace GameLibrary.Entity.Entities;

public class Platform : BaseEntity
{
    public string? Name { get; set; }
    public string? Manufacturer { get; set; }
    public int? ReleaseYear { get; set; }
    
    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public Platform() { }

    public Platform(string name, string manufacturer)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Invalid platform name (empty).", LogLevel.Error);
        }
        
        Name = name;
        Manufacturer = manufacturer;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty platform name.", LogLevel.Error);
            return;
        }
        Name = name;
    }

    public override void PrintInformation()
    {
        Console.WriteLine($"Platform: {Id}, {Name}, {Manufacturer}");
    }
}
