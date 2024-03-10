using System.Net;
using GamesGlobal.Core.Exceptions;
using GamesGlobal.Core.Repositories;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Shared.Enums;
using GamesGlobal.Shared.Extensions;
using GamesGlobal.Shared.Models.RequestModels;
using GamesGlobal.Shared.Models.ResponseModels;

namespace GamesGlobal.Core.Managers;

public class UserManager(IUserRepository userRepository, IAccessTokenManager accessTokenManager) : IUserManager
{
    public async Task<Auth> CreateNewUser(RegisterUserRequest registerUserRequest)
    {
        await ValidateNewUserRequest(registerUserRequest);

        var tokenGuid = Guid.NewGuid();
        var newUser = await userRepository.Insert(new UserEntity
        {
            EmailAddress = registerUserRequest.EmailAddress.Encrypt()!,
            EmailSearchHash = registerUserRequest.EmailAddress.HashSearchable()!,
            Name = registerUserRequest.Name.Encrypt()!,
            Password = registerUserRequest.Password.Hash(tokenGuid)!,
            OAuthProvider = "JWT",
            OAuthId = "",
            RefreshToken = "",
            TokenGuid = tokenGuid,
            Role = (int)Roles.User
        });

        var token = accessTokenManager.GenerateToken(newUser);
        var refreshToken = accessTokenManager.GenerateRefreshToken();

        newUser.OAuthId = token;
        newUser.RefreshToken = refreshToken;
        await userRepository.Update(newUser);

        return new Auth
        {
            AuthToken = token,
            RefreshToken = refreshToken
        };
    }

    public async Task<Auth> Login(LoginRequest loginRequest)
    {
        var existingUser = await userRepository.GetByEmail(loginRequest.Email);
        
        if (existingUser == null)
            throw new ApiObjectException("Invalid username or password", HttpStatusCode.NotFound);

        var passwordHash = loginRequest.Password.Hash(existingUser.TokenGuid);
        if (passwordHash != existingUser.Password)
            throw new ApiObjectException("Invalid username or password", HttpStatusCode.NotFound);
        
        if (existingUser.OAuthProvider != "JWT")
            throw new ApiObjectException("Invalid auth type", HttpStatusCode.UnprocessableEntity);

        return new Auth
        {
            AuthToken = existingUser.OAuthId,
            RefreshToken = existingUser.RefreshToken
        };
    }

    private async Task ValidateNewUserRequest(RegisterUserRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.EmailAddress))
            throw new ApiObjectException("Email address is required", HttpStatusCode.UnprocessableEntity);
        
        if (string.IsNullOrWhiteSpace(request.Password) || string.IsNullOrWhiteSpace(request.ConfirmPassword))
            throw new ApiObjectException("Passwords not supplied", HttpStatusCode.UnprocessableEntity);
        
        if (request.Password != request.ConfirmPassword)
            throw new ApiObjectException("Passwords don't match", HttpStatusCode.UnprocessableEntity);
        
        var existingUser = await userRepository.GetByEmail(request.EmailAddress);
        
        if (existingUser != null)
            throw new ApiObjectException("User already exists", HttpStatusCode.Conflict);
    }
}