using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("order_items", Schema = "dto")]
public class OrderItemEntity : BaseEntity
{
    [Required]
    public long OrderId { get; set; }
    
    [Required]
    public long GameId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [ForeignKey("OrderId")]
    public virtual OrderEntity Order { get; set; }
    
    [ForeignKey("GameId")]
    public virtual GameEntity Game { get; set; }
}