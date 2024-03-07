using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Core.Repositories;

public interface IGamesRepository : IReadWriteRepository<GameEntity>
{
    Task<IEnumerable<GameEntity>> GetAll(int page = 1, int pageSize = 50);
    Task<IEnumerable<GameEntity>> SearchGamesBy(string searchTerm);
    
}