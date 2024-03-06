using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Dal.Persistence;

public interface IGeneralUnitOfWork : IDisposable
{
    IRepository<GameEntity> Games { get; }
    IRepository<UserEntity> Users { get; }
}