using AutoMapper;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Mappers.Profiles;

public class ShoppingCartProfile : Profile
{
    public ShoppingCartProfile()
    {
        CreateMap<ShoppingCartEntity, ShoppingCart>()
            .ForMember(m => m.CartItems, opt => opt
                .MapFrom(x => x.CartItems.ToList()));

        CreateMap<CartItemEntity, CartItem>()
            .ForMember(m => m.Game, opts => 
                opts.MapFrom(x => x.Game));

        CreateMap<CartItem, CartItemEntity>()
            .ForMember(m => m.Game, opt => opt.Ignore())
            .ForMember(m => m.ShoppingCart, opt => opt.Ignore());
    }
}