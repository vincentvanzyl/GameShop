﻿using GamesGlobal.Core.Repositories.Base;
using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Core.Repositories;

public interface IShoppingCartRepository
{
    Task<ShoppingCartEntity> Upsert(ShoppingCartEntity shoppingCart);
    Task<ShoppingCartEntity?> GetByUserId(long userId);
}