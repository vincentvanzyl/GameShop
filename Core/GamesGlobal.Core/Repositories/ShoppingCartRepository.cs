using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Dal.Persistence;

namespace GamesGlobal.Core.Repositories;

public class ShoppingCartRepository : BaseRepository<ShoppingCartEntity>, IShoppingCartRepository
{
    public ShoppingCartRepository(IGeneralUnitOfWork generalUnitOfWork) : base(generalUnitOfWork)
    {
    }

    protected override IRepository<ShoppingCartEntity> Repository => _generalUnitOfWork.ShoppingCarts;

    public Task<ShoppingCartEntity?> GetByUserId(long userId) => 
        Repository.GetOne(x => x.UserId == userId);
}