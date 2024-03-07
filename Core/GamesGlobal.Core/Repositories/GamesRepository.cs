using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Core.Repositories;

public class GamesRepository : BaseRepository<GameEntity>, IGamesRepository
{
    public GamesRepository(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
    {
    }

    protected override IRepository<GameEntity> Repository => _generalUnitOfWork.Games;

    public async Task<IEnumerable<GameEntity>> GetAll(int page = 1, int pageSize = 50)
    {
        var skip = pageSize * (page - 1);
        return await Repository.GetQueryable(x => true).Skip(skip).Take(pageSize).ToListAsync();
    }

    public Task<IEnumerable<GameEntity>> SearchGamesBy(string searchTerm) =>
        Repository.Get(x => x.Title.ToLower().Contains(searchTerm));
}