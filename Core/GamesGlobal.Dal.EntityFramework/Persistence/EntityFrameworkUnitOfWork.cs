using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;

namespace GamesGlobal.Dal.EntityFramework.Persistence;

public class EntityFrameworkUnitOfWork : IGeneralUnitOfWork
{
    private readonly GamesGlobalContext _dbContext;
    
    public IRepository<GameEntity> Games { get; }
    public IRepository<UserEntity> Users { get; }

    public EntityFrameworkUnitOfWork(GamesGlobalContext dbContext)
    {
        _dbContext = dbContext;

        Games = new EntityFrameworkRepository<GameEntity>(dbContext, dbContext.Games);
        Users = new EntityFrameworkRepository<UserEntity>(dbContext, dbContext.Users);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.Collect();
    }
}