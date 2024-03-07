using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Dal.Persistence;

public interface IGeneralUnitOfWork : IDisposable
{
    IRepository<GameEntity> Games { get; }
    IRepository<UserEntity> Users { get; }
    IRepository<CartItemEntity> CartItems { get; set; }
    IRepository<OrderEntity> Orders { get; set; }
    IRepository<OrderItemEntity> OrderItems { get; set; }
    IRepository<ShoppingCartEntity> ShoppingCarts { get; set; }
}