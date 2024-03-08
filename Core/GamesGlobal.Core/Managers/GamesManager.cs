using System.Net;
using AutoMapper;
using GamesGlobal.Core.Exceptions;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;
using Microsoft.AspNetCore.Http;

namespace GamesGlobal.Core.Managers;

public class GamesManager : IGamesManager
{
    private readonly IGamesRepository _gamesRepository;
    private readonly IMapper _mapper;

    public GamesManager(IGamesRepository gamesRepository, IMapper mapper)
    {
        _gamesRepository = gamesRepository;
        _mapper = mapper;
    }
    
    #region Implementation of IGamesManager

    public async Task<List<Game>> GetAllGames()
    {
        var games = await _gamesRepository.SearchGamesBy("Cou");

        return games.Select(x => _mapper.Map<Game>(x)).ToList();
    }

    public async Task CreateGame(CreateGameRequest gameRequest)
    {
        var imageFile = gameRequest.Image;
        
        ValidateFileIsImage(imageFile);

        var entity = _mapper.Map<GameEntity>(gameRequest);
        entity.Image = ConvertStreamToByteArray(imageFile);;
        
        await _gamesRepository.Insert(entity);
    }

    public Task Delete(long id) => _gamesRepository.Delete(id);

    #endregion
    
    private byte[] ConvertStreamToByteArray(IFormFile file)
    {
        using var ms = new MemoryStream();
        file.CopyTo(ms);
        var fileBytes = ms.ToArray();

        return fileBytes;
    }

    private void ValidateFileIsImage(IFormFile file)
    {
        if (file.ContentType == null || !file.ContentType.StartsWith("image/"))
        {
            throw new ApiObjectException("Invalid image", HttpStatusCode.BadRequest);
        }
    }
}