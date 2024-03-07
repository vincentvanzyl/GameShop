using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;

namespace GamesGlobal.Dal.EntityFramework.Persistence;

public class EntityFrameworkUnitOfWork : IGeneralUnitOfWork
{
    private readonly GamesGlobalContext _dbContext;
    
    public IRepository<GameEntity> Games { get; }
    public IRepository<UserEntity> Users { get; }
    public IRepository<CartItemEntity> CartItems { get; set; }
    public IRepository<OrderEntity> Orders { get; set; }
    public IRepository<OrderItemEntity> OrderItems { get; set; }
    public IRepository<ShoppingCartEntity> ShoppingCarts { get; set; }

    public EntityFrameworkUnitOfWork(GamesGlobalContext dbContext)
    {
        _dbContext = dbContext;

        Games = new EntityFrameworkRepository<GameEntity>(dbContext, dbContext.Games);
        Users = new EntityFrameworkRepository<UserEntity>(dbContext, dbContext.Users);
        CartItems = new EntityFrameworkRepository<CartItemEntity>(dbContext, dbContext.CartItems);
        Orders = new EntityFrameworkRepository<OrderEntity>(dbContext, dbContext.Orders);
        OrderItems = new EntityFrameworkRepository<OrderItemEntity>(dbContext, dbContext.OrderItems);
        ShoppingCarts = new EntityFrameworkRepository<ShoppingCartEntity>(dbContext, dbContext.ShoppingCarts);
    }

    public void Dispose()
    {
        _dbContext.Dispose();
        GC.Collect();
    }
}