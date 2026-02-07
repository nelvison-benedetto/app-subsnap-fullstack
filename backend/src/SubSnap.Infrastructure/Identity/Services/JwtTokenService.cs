using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SubSnap.Core.Abstractions.Identity;
using SubSnap.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SubSnap.Infrastructure.Identity.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _config;
    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }
    public string GenerateAccessToken(User user)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id!.Value.ToString()),  //! perche deve esistere
            //.Sub è Subject -> ID univoco dello user
            new Claim(ClaimTypes.NameIdentifier, user.Id!.Value.ToString()), //cosi aspnet sa sempre chi è l'utente
            new Claim(JwtRegisteredClaimNames.Email, user.Email.Value)
            //.Email è Email dello user
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["JwtSettings:SecretKey"]!));
        //carichi una secret key, la trasformi in byte[], la usi per firmare il token

        var token = new JwtSecurityToken(  //creazione JWT
            issuer: _config["JwtSettings:Issuer"],
            audience: _config["JwtSettings:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(10),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);  //serializzazione, return xxxxx.yyyyy.zzzzz
    }
    public string GenerateRefreshToken()
        => Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    //usa CSPRNG (crypto-secure), usa CSPRNG (crypto-secure), praticamente impossibile da indovinare
}
