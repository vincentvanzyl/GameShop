using System.Net;
using AutoMapper;
using GamesGlobal.Core.Exceptions;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Managers;

public class ShoppingCartManager : IShoppingCartManager
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly IShoppingCartItemRepository _shoppingCartItemRepository;
    private readonly IGamesManager _gamesManager;
    private readonly IMapper _mapper;

    public ShoppingCartManager(IShoppingCartRepository shoppingCartRepository, IShoppingCartItemRepository shoppingCartItemRepository, IMapper mapper, IGamesManager gamesManager)
    {
        _shoppingCartRepository = shoppingCartRepository;
        _shoppingCartItemRepository = shoppingCartItemRepository;
        _mapper = mapper;
        _gamesManager = gamesManager;
    }

    public async Task<ShoppingCart> Get(long userId)
    {
        var entity = await _shoppingCartRepository.GetByUserId(userId) ?? await _shoppingCartRepository.Insert(new ShoppingCartEntity
        {
            UserId = userId
        });

        return _mapper.Map<ShoppingCart>(entity);
    }

    public async Task AddItem(long userId, CartItem cartItem)
    {
        var cart = await _shoppingCartRepository.GetByUserId(userId);

        var entity = _mapper.Map<CartItemEntity>(cartItem);
        entity.CartId = cart.Id;

        await _shoppingCartItemRepository.Insert(entity);
    }

    public async Task RemoveItem(long userId,long cartItemId)
    {
        var cart = await _shoppingCartRepository.GetByUserId(userId);
        var cartItem = await _shoppingCartItemRepository.GetById(cartItemId);

        if (cart.Id != cartItem.CartId)
            throw new ApiObjectException("Unathorzied", HttpStatusCode.Unauthorized);
        
        await _shoppingCartItemRepository.Delete(cartItemId);
    }

    public async Task SetQuantity(long userId, long cartItemId, int quantity)
    {
        var cart = await _shoppingCartRepository.GetByUserId(userId);
        var cartItem = await _shoppingCartItemRepository.GetById(cartItemId);

        if (cart.Id != cartItem.CartId)
            throw new ApiObjectException("Unathorzied", HttpStatusCode.Unauthorized);

        cartItem.Quantity = quantity;
        await _shoppingCartItemRepository.Update(cartItem);
    }
}