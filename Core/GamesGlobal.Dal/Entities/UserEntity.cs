using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GamesGlobal.Dal.Entities.Base;

namespace GamesGlobal.Dal.Entities;

[Table("users", Schema = "dto")]
public class UserEntity : BaseEntity
{
    [MaxLength(100)]
    public string Name { get; set; }
    
    public string EmailAddress { get; set; }
    public string Password { get; set; }
    
    public int Role { get; set; }
    
    public string OAuthProvider { get; set; }
    public string OAuthId { get; set; }
    public string RefreshToken { get; set; }
    public Guid TokenGuid { get; set; }
    public string EmailSearchHash { get; set; }

    public virtual ICollection<ShoppingCartEntity> ShoppingCarts { get; set; }
    public virtual ICollection<OrderEntity> Orders { get; set; }
}