using GamesGlobal.Dal.Entities;
using GamesGlobal.Utilities.Config;
using Microsoft.EntityFrameworkCore;

namespace GamesGlobal.Dal.EntityFramework;

public class GamesGlobalContext : DbContext
{
    public GamesGlobalContext() {}
    public GamesGlobalContext(DbContextOptions<GamesGlobalContext> options) : base(options)
    {
        
    }

    public DbSet<GameEntity> Games { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CartItemEntity> CartItems { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<ShoppingCartEntity> ShoppingCarts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(GamesGlobalSettings.Instance.ConnectionString);
    }
}