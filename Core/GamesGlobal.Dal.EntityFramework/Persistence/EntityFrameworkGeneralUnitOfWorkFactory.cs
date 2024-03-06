using GamesGlobal.Dal.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Dal.EntityFramework.Persistence;

public class EntityFrameworkGeneralUnitOfWorkFactory : IGeneralUnitOfWorkFactory
{
    private readonly Lazy<IGeneralUnitOfWork> _singleConnection;
    private readonly string _connectionString;

    public EntityFrameworkGeneralUnitOfWorkFactory(string connectionString)
    {
        _connectionString = connectionString;
        _singleConnection = new Lazy<IGeneralUnitOfWork>(GeneralUnitOfWork);
    }
    
    public IGeneralUnitOfWork GetConnection() => _singleConnection.Value;


    private IGeneralUnitOfWork GeneralUnitOfWork()
    {
        var contextOptions = new DbContextOptionsBuilder<GamesGlobalContext>()
            .UseSqlServer(_connectionString)
            .Options;
        
        var dbContext = new GamesGlobalContext(contextOptions);
        return new EntityFrameworkUnitOfWork(dbContext);
    }
}