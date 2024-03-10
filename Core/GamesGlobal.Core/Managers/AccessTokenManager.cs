using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using GamesGlobal.Dal.Entities;
using GamesGlobal.Utilities.Config;
using Microsoft.IdentityModel.Tokens;

namespace GamesGlobal.Core.Managers;

public class AccessTokenManager(IGamesGlobalSettings settings) : IAccessTokenManager
{
    public string GenerateRefreshToken(int size = 32)
    {
        var randomNumber = new byte[size];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    public string GenerateToken(UserEntity user)
    {
        //Security key
        string securityKey = settings.Security.StrongKey; // Should change to it's own key, bit simpler for now

        //Symmetric security key
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));

        //Signing token
        var signingCredentials =
            new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature);
                
        //Add claims
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Id.ToString()),
            new Claim(ClaimTypes.Role, user.Role.ToString())
        };

        var expiryDate = DateTime.Now.AddDays(30);

        //Create token
        var token = new JwtSecurityToken(
            issuer: "22seven",
            audience: "readers",
            expires: expiryDate,
            signingCredentials: signingCredentials,
            claims: claims
        );

        //Return token
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}