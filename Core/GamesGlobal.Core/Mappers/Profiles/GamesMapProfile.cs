using AutoMapper;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;

namespace GamesGlobal.Core.Mappers.Profiles;

public class GamesMapProfile : Profile
{
    public GamesMapProfile()
    {
        CreateMap<GameEntity, Game>().ReverseMap();
    }
}