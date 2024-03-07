using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("shopping_cart_items", Schema = "dto")]
public class CartItemEntity : BaseEntity
{
    [Required]
    public long CartId { get; set; }
    
    [Required]
    public long GameId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public double Price { get; set; }
    
    [ForeignKey("GameId")]
    public virtual GameEntity Game { get; set; }
    
    [ForeignKey("CartId")]
    public virtual ShoppingCartEntity ShoppingCart { get; set; }
}