using GamesGlobal.Core.Managers;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers;

[Route($"[controller]")]
public class GamesController : Controller
{
    private readonly IGamesManager _gamesManager;

    public GamesController(IGamesManager gamesManager)
    {
        _gamesManager = gamesManager;
    }

    [HttpGet("all")]
    public async Task<List<Game>> SearchGames()
    {
        return await _gamesManager.GetAllGames();
    }

    [HttpPost("add")]
    public async Task Add([FromBody] Game game)
    {
        await _gamesManager.Insert(game);
    }
}