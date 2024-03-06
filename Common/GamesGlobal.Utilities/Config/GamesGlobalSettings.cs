using Microsoft.Extensions.Configuration;

namespace GamesGlobal.Utilities.Config;

public class GamesGlobalSettings : IGamesGlobalSettings
{
    private readonly IConfiguration _configuration;
    private static Lazy<GamesGlobalSettings> _instance = new();
    
    public GamesGlobalSettings()
    {
		
    }
    
    public GamesGlobalSettings(IConfiguration configuration)
    {
        _configuration = configuration;
        SetInstance(_configuration);
    }
    
    public static void SetInstance(IConfiguration configuration)
    {
        _instance = new Lazy<GamesGlobalSettings>(() => new GamesGlobalSettings(configuration));
    }

    #region Singleton

    public static GamesGlobalSettings Instance => _instance.Value;

    public string ConnectionString => _configuration.GetConnectionString("GameShopDb") ?? string.Empty;

    #endregion
}