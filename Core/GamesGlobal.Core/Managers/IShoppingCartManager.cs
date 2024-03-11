using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Managers;

public interface IShoppingCartManager
{
    Task<ShoppingCart> Get(long userId);
    Task AddItem(long userId, CartItem cartItem);
    Task RemoveItem(long userId,long cartItemId);
    Task SetQuantity(long userId,long cartItemId, int quantity);
}