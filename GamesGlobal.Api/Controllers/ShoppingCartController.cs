using GamesGlobal.Core.Managers;
using GamesGlobal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace GamesGlobal.Api.Controllers;

[Route("[controller]")]
public class ShoppingCartController(IShoppingCartManager shoppingCartManager) : Controller
{
    private readonly IShoppingCartManager _shoppingCartManager = shoppingCartManager;

    [HttpGet]
    public async Task<List<CartItem>> VIewShoppingCar()
    {
        var cart = await _shoppingCartManager.Get(1);

        return cart.CartItems;
    }

    [HttpPost("add")]
    public async Task Add([FromBody] CartItem cartItem) => await _shoppingCartManager.AddItem(cartItem);

    [HttpDelete("{cartItemId}")]
    public async Task Remove([FromRoute] long cartItemId) => await _shoppingCartManager.RemoveItem(cartItemId);
}