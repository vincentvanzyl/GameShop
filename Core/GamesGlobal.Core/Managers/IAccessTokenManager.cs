using GamesGlobal.Dal.Entities;

namespace GamesGlobal.Core.Managers;

public interface IAccessTokenManager
{
    string GenerateRefreshToken(int size = 32);
    string GenerateToken(UserEntity user);
}