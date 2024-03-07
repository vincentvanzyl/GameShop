using AutoMapper;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;

namespace GamesGlobal.Core.Mappers.Profiles;

public class GamesMapProfile : Profile
{
    public GamesMapProfile()
    {
        CreateMap<GameEntity, Game>()
            .ForMember(m => m.Image, options => 
                options.MapFrom(e => Convert.ToBase64String(e.Image)));
        
        CreateMap<CreateGameRequest, GameEntity>()
            .ForMember(m => m.Image, options => options.Ignore());
    }
}