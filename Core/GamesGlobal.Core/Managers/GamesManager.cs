using AutoMapper;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Managers;

public class GamesManager : IGamesManager
{
    private readonly IGamesRepository _gamesRepository;
    private readonly IMapper _mapper;

    public GamesManager(IGamesRepository gamesRepository, IMapper mapper)
    {
        _gamesRepository = gamesRepository;
        _mapper = mapper;
    }

    public async Task<List<Game>> GetAllGames()
    {
        var games = await _gamesRepository.SearchGamesBy("Cou");

        return games.Select(x => _mapper.Map<Game>(x)).ToList();
    }

    public async Task Insert(Game game)
    {
        var entity = _mapper.Map<GameEntity>(game);

        await _gamesRepository.Insert(entity);
    }
}