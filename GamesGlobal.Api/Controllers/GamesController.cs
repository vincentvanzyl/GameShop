using GamesGlobal.Core.Managers;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;
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
    public async Task<List<Game>> SearchGames() =>
        await _gamesManager.GetAllGames();
    

    [HttpPost("add")]
    public async Task Add([FromForm] CreateGameRequest gameRequest) =>
        await _gamesManager.CreateGame(gameRequest);
    
}