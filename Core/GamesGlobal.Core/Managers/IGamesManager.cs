using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;

namespace GamesGlobal.Core.Managers;

public interface IGamesManager
{
    Task<List<Game>> GetAllGames();
    Task<Game?> GetById(long id);
    Task CreateGame(CreateGameRequest gameRequest);
    Task Delete(long id);
}