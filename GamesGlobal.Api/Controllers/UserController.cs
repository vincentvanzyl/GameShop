using GamesGlobal.Shared.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers;

[Route("[controller]")]
public class UserController(IUserManager userManager) : Controller
{
    [HttpPost("register")]
    public async Task Register([FromBody] RegisterUserRequest register)
    {
        
    }

    [HttpPost("login")]
    public async Task Authenticate([FromBody] LoginRequest login)
    {
        
    }
}