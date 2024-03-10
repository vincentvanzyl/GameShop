using GamesGlobal.Core.Managers;
using GamesGlobal.Shared.Extensions;
using GamesGlobal.Shared.Models.RequestModels;
using GamesGlobal.Shared.Models.ResponseModels;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers;

[Route("[controller]")]
public class UserController(IUserManager userManager) : Controller
{
    [HttpPost("register")]
    public async Task<Auth> Register([FromBody] RegisterUserRequest register) =>
        await userManager.CreateNewUser(register);

    [HttpPost("login")]
    public async Task<Auth> Authenticate([FromBody] LoginRequest login) =>
        await userManager.Login(login);
}