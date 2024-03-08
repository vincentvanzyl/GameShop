using Microsoft.AspNetCore.Http;

namespace GamesGlobal.Shared.Models.RequestModels;

public class CreateGameRequest
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public int Rating { get; set; }
    public double Price { get; set; }
    public IFormFile Image { get; set; }
}