using GamesGlobal.Api.Attributes;
using GamesGlobal.Api.Controllers.Base;
using GamesGlobal.Core.Managers;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers;

[Route($"[controller]")]
public class GamesController(IGamesManager gamesManager, IUserManager userManager) : BaseController(userManager)
{
    [Authorize]
    [HttpGet("all")]
    public async Task<List<Game>> GetAll() =>
        await gamesManager.GetAllGames();

    [AuthorizeAdminKey]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpPost("add")]
    public async Task Add([FromForm] CreateGameRequest gameRequest) =>
        await gamesManager.CreateGame(gameRequest);
    
    [AuthorizeAdminKey]
    [ApiExplorerSettings(IgnoreApi = true)]
    [HttpDelete("{id}")]
    public async Task Remove([FromRoute] long id) =>
        await gamesManager.Delete(id);
}