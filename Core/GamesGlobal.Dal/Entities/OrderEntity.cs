using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("orders", Schema = "dto")]
public class OrderEntity : BaseEntity
{
    [Required]
    public long UserId { get; set; }
    
    [Required]
    public double TotalAmount { get; set; }
    
    [ForeignKey("UserId")]
    public virtual UserEntity User { get; set; }
    
    public virtual ICollection<OrderItemEntity> OrderItems { get; set; }
}