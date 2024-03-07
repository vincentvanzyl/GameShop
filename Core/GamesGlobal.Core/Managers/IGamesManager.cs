using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Managers;

public interface IGamesManager
{
    Task<List<Game>> GetAllGames();
    Task Insert(Game game);
}