namespace PatternsLab.Problems.Singleton;

public class AppConfig
{
    public static int LoadCount;

    public string DbConnection { get; set; }
    public string Theme { get; set; }

    private static readonly object _lock = new(); // use lock to make it thread-safe

    private static AppConfig? _instance;
    public static AppConfig Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance is null)
                    _instance = new AppConfig(); //lazy

                return _instance;
            }
        }
    }

    private AppConfig()
    {
        LoadCount++;
        Console.WriteLine($"[AppConfig] Loading settings from disk... (load #{LoadCount})");
        Thread.Sleep(300);
        DbConnection = "Server=localhost;Db=School";
        Theme = "Light";
    }
}

public class DatabaseService
{
    public AppConfig Config = AppConfig.Instance;

    public void Connect() => Console.WriteLine($"Connecting to {Config.DbConnection}");
}

public class UiService
{
    public AppConfig Config = AppConfig.Instance;

    public void Render() => Console.WriteLine($"UI is using theme: {Config.Theme}");
}
