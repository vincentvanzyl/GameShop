using GamesGlobal.Shared.Models;
using GamesGlobal.Shared.Models.RequestModels;
using GamesGlobal.Shared.Models.ResponseModels;

namespace GamesGlobal.Core.Managers;

public interface IUserManager
{
    Task<Auth> CreateNewUser(RegisterUserRequest registerUserRequest);
    Task<Auth> Login(LoginRequest loginRequest);
    Task<User> GetById(long id);
}