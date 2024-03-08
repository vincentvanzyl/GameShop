namespace GamesGlobal.Shared.Models;

public class CartItem
{
    public long Id { get; set; }
    public long CartId { get; set; }
    public long GameId { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    
    public Game Game { get; set; }
}