using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("games", Schema = "dto")]
public class GameEntity : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public byte[] Image { get; set; }
    public string Genre { get; set; }
    public DateTime ReleaseDate { get; set; }
    public double Price { get; set; }
}