using System.Security.Claims;
using GamesGlobal.Core.Managers;
using GamesGlobal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers.Base;

public class BaseController(IUserManager userManager) : Controller
{
    protected long GetLoggedInUserId()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;
        return long.Parse(userId);
    }
    
    protected async Task<User?> GetLoggedInUser()
    {
        var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name)?.Value;

        if (!string.IsNullOrWhiteSpace(userId))
        {
            var obj = await userManager.GetById(long.Parse(userId));
            return obj;
        }

        return null;
    }

    
}