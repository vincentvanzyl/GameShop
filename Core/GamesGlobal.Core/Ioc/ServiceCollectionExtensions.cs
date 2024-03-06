using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.EntityFramework;
using GamesGlobal.Dal.EntityFramework.Persistence;
using GamesGlobal.Dal.Persistence;
using GamesGlobal.Utilities.Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GamesGlobal.Core.Ioc;

public static class ServiceCollectionExtensions
{
    public static void AddCoreServices(this IServiceCollection services)
    {
        AddCoreConfiguration(services);
        AddAutoMapper(services);
        AddDatabase(services);
        AddRepositories(services);
    }
    
    private static void AddCoreConfiguration(IServiceCollection services)
    {
        services.AddSingleton<IGamesGlobalSettings, GamesGlobalSettings>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddSingleton<IGamesRepository, GamesRepository>();
    }

    private static void AddAutoMapper(IServiceCollection services)
    {
        
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