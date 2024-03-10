using AutoMapper;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Managers;

public class ShoppingCartManager : IShoppingCartManager
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
    private readonly IMapper _mapper;

    public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository, IShoppingCartItemRepository shoppingCartItemRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _shoppingCartItemRepository = shoppingCartItemRepository;
    }

    public async Task<ShoppingCart> Get(long userId)
    {
        var entity = await _shoppingCartRepository.GetByUserId(userId) ?? await _shoppingCartRepository.Insert(new ShoppingCartEntity
        {
            UserId = userId
        });

        return _mapper.Map<ShoppingCart>(entity);
    }

    public Task AddItem(CartItem cartItem)
    {
        throw new NotImplementedException();
    }

    public Task RemoveItem(long cartItemId)
    {
        throw new NotImplementedException();
    }

    public Task SetQuantity(long cartItemId, int quantity)
    {
        throw new NotImplementedException();
    }
}