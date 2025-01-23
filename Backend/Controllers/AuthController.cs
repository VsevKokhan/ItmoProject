using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IUserService userService;
    private TokenService tokenService;
    private readonly int accessTokenLifetime;
    private readonly int RefreshTokenLifetime;
    public AuthController(IUserService userService, TokenService tokenService, IConfiguration configuration)
    {
        this.userService = userService;
        this.tokenService = tokenService;
        var jwtSettings = configuration.GetSection("JWT");
        RefreshTokenLifetime = int.Parse(jwtSettings["RefreshTokenLifetime"]);
    }
    
    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserDto user)
    {
        var newUser = userService.Add(user);
        
        var accesToken = tokenService.GenerateAccessToken(newUser.Id);
        var refreshToken = tokenService.GenerateRefreshToken(newUser.Id);
        
        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,  // Запрещает доступ к куке через JavaScript
            //Secure = true,    // Кука будет передаваться только по HTTPS
            SameSite = SameSiteMode.Strict, // Запрещает отправку куки в сторонние запросы
            Expires = DateTime.UtcNow.AddMinutes(RefreshTokenLifetime) // Время жизни refresh token
        });
        Response.Headers.Append("Authorization", $"Bearer {accesToken}");
        return Ok();
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserForLogin user)
    {
        var mail = user.Mail;
        var pas = user.Pass;
        var userEntity = userService.Get(pas, mail);
        if (userEntity == null)
        {
            return BadRequest("User is not exist");
        }
        var accessToken = tokenService.GenerateAccessToken(userEntity.Id);
        var refreshToken = tokenService.GenerateRefreshToken(userEntity.Id);
        Response.Cookies.Append("RefreshToken", refreshToken, new CookieOptions
        {
            HttpOnly = true,  // Запрещает доступ к куке через JavaScript
            //Secure = true,    // Кука будет передаваться только по HTTPS
            SameSite = SameSiteMode.Strict, // Запрещает отправку куки в сторонние запросы
            Expires = DateTime.UtcNow.AddMinutes(RefreshTokenLifetime) // Время жизни refresh token
        });
        Response.Headers.Append("Authorization", $"Bearer {accessToken}");
        return Ok();
        
    }
    [Authorize]
    [HttpGet("GetUserByToken")]
    public IActionResult Get()
    {
        var id = User.FindFirstValue("id");
        var user = userService.Get(int.Parse(id));

        return Ok(user);
    }
    [HttpGet("Refresh")]
    public IActionResult Refresh()
    {
        const string refreshTokenCookieName = "RefreshToken";

        // Извлекаем токен из куки
        if (!HttpContext.Request.Cookies.TryGetValue(refreshTokenCookieName, out var refreshToken))
        {
            return RedirectToAction("Login");
        }

        var id = tokenService.ValidateRefreshToken(refreshToken).FindFirstValue("id");
        
        var newAccessToken = tokenService.GenerateAccessToken(int.Parse(id));
        var newRefreshToken = tokenService.GenerateRefreshToken(int.Parse(id));
        Response.Cookies.Append("RefreshToken", newRefreshToken, new CookieOptions
        {
            HttpOnly = true,  // Запрещает доступ к куке через JavaScript
            //Secure = true,    // Кука будет передаваться только по HTTPS
            SameSite = SameSiteMode.Strict, // Запрещает отправку куки в сторонние запросы
            Expires = DateTime.UtcNow.AddMinutes(RefreshTokenLifetime) // Время жизни refresh token
        });
        Response.Headers.Append("Authorization", $"Bearer {newAccessToken}");
        return Ok();
    }

}