using GameLibrary.Integration.Logger;

namespace GameLibrary.Entity.Entities;

public class Genre : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    // Navigation properties
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public Genre() { }

    public Genre(string name, string? description = null)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Invalid genre name (empty).", LogLevel.Error);
        }
        
        Name = name;
        Description = description;
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty genre name.", LogLevel.Error);
            return;
        }
        Name = name;
    }

    public override void PrintInformation()
    {
        Console.WriteLine($"Genre: {Id}, {Name}, {Description}");
    }
}
