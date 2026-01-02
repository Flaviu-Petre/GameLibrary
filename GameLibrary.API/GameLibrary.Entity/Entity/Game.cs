using GameLibrary.Integration.Logger;

namespace GameLibrary.Entity.Entities;

public class Game : BaseEntity
{
    public string? Title { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string? Description { get; set; }
    
    // Foreign keys
    public int? DeveloperId { get; set; }
    public int? PublisherId { get; set; }
    public int? PlatformId { get; set; }

    // Navigation properties
    public virtual Developer? Developer { get; set; }
    public virtual Publisher? Publisher { get; set; }
    public virtual Platform? Platform { get; set; }
    public virtual ICollection<User> Users { get; set; } = new List<User>();
    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public Game() { }

    public Game(string title, string description, DateTime releaseDate)
    {
        if (string.IsNullOrEmpty(title))
        {
            LoggerSingleton.GetInstance().Log("Invalid game title (empty).", LogLevel.Error);
        }
        
        Title = title;
        Description = description;
        ReleaseDate = releaseDate;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            LoggerSingleton.GetInstance().Log("Attempted to set empty game title.", LogLevel.Error);
            return;
        }
        Title = title;
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public override void PrintInformation()
    {
        Console.WriteLine($"Game: {Id}, {Title}, {ReleaseDate:d}, {Description}");
    }
}
