using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using UserManager.Models;
using UserManager.Settings;

namespace UserManager.Service;

public class TokenService
{
    private UserSettings _userSettings;

    public TokenService(IConfiguration config)
    {
        // ref: https://learn.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-6.0&tabs=linux#register-the-user-secrets-configuration-source
        _userSettings = config.GetSection("User").Get<UserSettings>();
    }
    public void GenerateToken(User user)
    {   
        // building the token content.
        Claim[] claims = new Claim[] {
            new Claim("username", user.UserName),
            new Claim("id", user.Id),
            new Claim(ClaimTypes.DateOfBirth, user.Birthdate.ToString()),
        };        

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_userSettings.TokenKey)
        );

        var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256 );

        JwtSecurityToken token  = new JwtSecurityToken(
            expires: DateTime.Now.AddMinutes(10), //The token will expires in 10 minutes
            claims: claims,
            signingCredentials: signingCredentials
        );
    }
}
