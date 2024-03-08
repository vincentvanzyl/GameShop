using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Managers;

public interface IShoppingCartManager
{
    Task<ShoppingCart> Get(long userId);
    Task AddItem(CartItem cartItem);
    Task RemoveItem(long cartItemId);
    Task SetQuantity(long cartItemId, int quantity);
}