namespace GamesGlobal.Shared.Models;

public class ShoppingCart
{
    public long Id { get; set; }
    public long UserId { get; set; }
    
    public List<CartItem> CartItems { get; set; }
}