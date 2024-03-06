using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("games", Schema = "dto")]
public class GameEntity : BaseEntity
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public string Image { get; set; }
}