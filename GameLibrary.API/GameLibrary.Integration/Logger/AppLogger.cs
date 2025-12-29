namespace GameLibrary.Integration.Logger;

public enum LogLevel
{
    Info,
    Warning,
    Error
}

public interface IAppLogger
{
    void Log(string message, LogLevel level = LogLevel.Info);
    void SetMinimumLevel(LogLevel level);
}

public class AppLogger : IAppLogger
{
    private readonly StreamWriter _writer;
    private LogLevel _minimumLevel = LogLevel.Info;
    private readonly object _lock = new();

    public AppLogger(string filename = "app.log")
    {
        _writer = new StreamWriter(filename, append: true) { AutoFlush = true };
    }

    public void Log(string message, LogLevel level = LogLevel.Info)
    {
        if (level < _minimumLevel)
            return;

        lock (_lock)
        {
            var levelStr = level switch
            {
                LogLevel.Info => "[INFO]   ",
                LogLevel.Warning => "[WARNING]",
                LogLevel.Error => "[ERROR]  ",
                _ => "[INFO]   "
            };

            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            _writer.WriteLine($"{levelStr} [{timestamp}] {message}");
        }
    }

    public void SetMinimumLevel(LogLevel level)
    {
        _minimumLevel = level;
    }
}
