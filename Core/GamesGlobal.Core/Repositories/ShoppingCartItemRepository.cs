using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;

namespace GamesGlobal.Core.Repositories;

public class ShoppingCartItemRepository : BaseRepository<CartItemEntity>, IShoppingCartItemRepository
{
    public ShoppingCartItemRepository(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
    {
    }

    protected override IRepository<CartItemEntity> Repository => _generalUnitOfWork.CartItems;
}