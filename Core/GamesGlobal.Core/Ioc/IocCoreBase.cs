using GamesGlobal.Core.Managers;
using GamesGlobal.Core.Mappers.Profiles;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.EntityFramework;
using GamesGlobal.Dal.EntityFramework.Persistence;
using GamesGlobal.Dal.Persistence;
using GamesGlobal.Utilities.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.Core.Ioc;

public static class IocCoreBase
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        AddCoreConfiguration(services);
        AddAutoMapper(services);
        AddDatabase(services);
        AddRepositories(services);
        AddManagers(services);
    }

    private static void AddManagers(IServiceCollection services)
    {
        services.AddScoped<IGamesManager, GamesManager>();
        services.AddScoped<IShoppingCartManager, ShoppingCartManager>();
        services.AddScoped<IUserManager, UserManager>();
        services.AddScoped<IShoppingCartManager, ShoppingCartManager>();
        services.AddScoped<IAccessTokenManager, AccessTokenManager>();
    }

    private static void AddCoreConfiguration(IServiceCollection services)
    {
        services.AddSingleton<IGamesGlobalSettings, GamesGlobalSettings>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IGamesRepository, GamesRepository>();
        services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
        services.AddScoped<IShoppingCartItemRepository, ShoppingCartItemRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<GamesMapProfile>();
            cfg.AddProfile<ShoppingCartProfile>();
        });
    }
    
    private static void AddDatabase(IServiceCollection services)
    {
        services.AddSingleton<IGeneralUnitOfWorkFactory>(sp =>
        {
            var config = sp.GetRequiredService<IGamesGlobalSettings>();

            return new EntityFrameworkGeneralUnitOfWorkFactory(config.ConnectionString);
        });
        services.AddSingleton<IGeneralUnitOfWork>(sp =>
        {
            var factory = sp.GetRequiredService<IGeneralUnitOfWorkFactory>();

            return factory.GetConnection();
        });
    }

    
}