using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;

namespace GamesGlobal.Core.Repositories;

public class GamesRepository : BaseRepository<GameEntity>, IGamesRepository
{
    public GamesRepository(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
    {
    }

    protected override IRepository<GameEntity> Repository => _generalUnitOfWork.Games;

    public Task<IEnumerable<GameEntity>> SearchGamesBy(string searchTerm) =>
        Repository.Get(x => x.Title.ToLower().Contains(searchTerm));
}