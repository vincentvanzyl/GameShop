namespace GamesGlobal.Utilities.Config;

public interface IGamesGlobalSettings
{
    static IGamesGlobalSettings Instance { get; }
    
    public string ConnectionString { get; }
}