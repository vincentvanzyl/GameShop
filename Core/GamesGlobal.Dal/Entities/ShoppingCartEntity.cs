using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("shopping_cart", Schema = "dto")]
public class ShoppingCartEntity : BaseEntity
{
    [Required]
    public long UserId { get; set; }
    
    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }
    
    public virtual ICollection<CartItemEntity> CartItems { get; set; }
}