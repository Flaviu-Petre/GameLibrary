namespace GameLibrary.Integration.Logger;

public static class LoggerSingleton
{
    private static IAppLogger? _instance;
    private static readonly object _lock = new();

    public static IAppLogger GetInstance()
    {
        if (_instance == null)
        {
            lock (_lock)
            {
                _instance ??= new AppLogger("gamelibrary.log");
            }
        }
        return _instance;
    }

    public static void SetInstance(IAppLogger logger)
    {
        lock (_lock)
        {
            _instance = logger;
        }
    }
}
