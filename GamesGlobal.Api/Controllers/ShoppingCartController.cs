using GamesGlobal.Api.Controllers.Base;
using GamesGlobal.Core.Managers;
using GamesGlobal.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers;

[Authorize]
[Route("[controller]")]
public class ShoppingCartController(IShoppingCartManager shoppingCartManager, IUserManager userManager) : BaseController(userManager)
{
    [HttpGet]
    public async Task<List<CartItem>> ViewShoppingCar()
    {
        var cart = await shoppingCartManager.Get(GetLoggedInUserId());
        return cart.CartItems;
    }

    [HttpPost("add")]
    public async Task Add([FromBody] CartItem cartItem) => await shoppingCartManager.AddItem(GetLoggedInUserId(), cartItem);

    [HttpDelete("{cartItemId}")]
    public async Task Remove([FromRoute] long cartItemId) => await shoppingCartManager.RemoveItem(GetLoggedInUserId(), cartItemId);
}