using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Core.Repositories;

public interface IGamesRepository : IReadWriteRepository<GameEntity>
{
    Task<IEnumerable<GameEntity>> SearchGamesBy(string searchTerm);
    Task Insert(GameEntity entity);
    Task Delete(long id);
}