using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;

namespace GamesGlobal.Core.Managers;

public interface IGamesManager
{
    Task<List<Game>> GetAllGames();
    Task CreateGame(CreateGameRequest gameRequest);
}