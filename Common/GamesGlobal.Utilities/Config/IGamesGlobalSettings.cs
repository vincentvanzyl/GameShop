using GamesGlobal.Utilities.Config.Models;

namespace GamesGlobal.Utilities.Config;

public interface IGamesGlobalSettings
{
    static IGamesGlobalSettings Instance { get; }
    
    public string ConnectionString { get; }
    
    public SecuritySettings Security { get; }
}