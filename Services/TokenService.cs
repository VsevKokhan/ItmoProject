using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Services;

public class TokenService
{
    private readonly string _key;
    private readonly string _issuer;
    private readonly string _audience;
    private readonly int _accessTokenLifetime;
    private readonly int _accessRefreshTokenLifetime;
    
    public TokenService(IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection("JWT");
        _key = jwtSettings["Key"];
        _issuer = jwtSettings["Issuer"];
        _audience = jwtSettings["Audience"];
        _accessTokenLifetime = int.Parse(jwtSettings["AccessTokenLifetime"]);
        
        _accessRefreshTokenLifetime = int.Parse(jwtSettings["AccessTokenLifetime"]);
    }
    public string GenerateToken(int id)
    {
        // Создаём список claims с логином и паролем
        var claims = new[]
        {
            new Claim("id", id.ToString())
        };

        // Генерация ключа подписи
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Настройки токена
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessTokenLifetime),
            signingCredentials: credentials);

        // Возвращаем сгенерированный токен
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken(int id)
    {
        var claims = new[]
        {
            new Claim("id", id.ToString())
        };

        // Генерация ключа подписи
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        // Настройки токена
        var token = new JwtSecurityToken(
            issuer: _issuer,
            audience: _audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_accessRefreshTokenLifetime),
            signingCredentials: credentials);

        // Возвращаем сгенерированный токен
        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateNewAccessToken(string refreshToken)
    {
        var id = GetIdFromToken(refreshToken);
        return GenerateToken(id);
    }
    public ClaimsPrincipal ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_key); 
        try
        {
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateLifetime = true
            };

            // Валидируем токен и извлекаем ClaimsPrincipal
            var principal = tokenHandler.ValidateToken(token, parameters, out _);
            return principal;
        }
        catch (Exception)
        {
            return null; // Если токен недействителен
        }
    }
    public int GetIdFromToken(string token)
    {
        var principal = ValidateToken(token);
        return int.Parse(principal?.FindFirstValue("id"));
    }

}