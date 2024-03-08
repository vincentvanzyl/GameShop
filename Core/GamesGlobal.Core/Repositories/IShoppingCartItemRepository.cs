using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Core.Repositories;

public interface IShoppingCartItemRepository : IReadWriteRepository<CartItemEntity>
{
    
}