namespace GamesGlobal.Shared.Models;

public class Game
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int Rating { get; set; }
    public double Price { get; set; }
    public string Image { get; set; }
}