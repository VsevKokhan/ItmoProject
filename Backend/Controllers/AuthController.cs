using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IUserService service;
    private TokenService tokenService;
    public AuthController(IUserService service, TokenService tokenService)
    {
        this.service = service;
        this.tokenService = tokenService;
    }
    
    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserDto user)
    {
        var newUser = service.Add(user);
        
        var token = tokenService.GenerateAccessToken(newUser.Id);
        var refreshtoken = tokenService.GenerateRefreshToken(newUser.Id);

        return Ok(new {AccessToken = token, RefreshToken = refreshtoken});
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserForLogin user)
    {
        var name = user.Name;
        var pas = user.Pass;
        var userEntity = service.Get(pas, name);
        
        var token = tokenService.GenerateAccessToken(userEntity.Id);
        var refreshtoken = tokenService.GenerateRefreshToken(userEntity.Id);
        return Ok(new {AccessToken = token, RefreshToken = refreshtoken});
    }
    [HttpPost("GetUserByToken")]
    public IActionResult Get([FromBody] string token)
    {
        var id = tokenService.GetIdFromToken(token);
        var user = service.Get(id);

        return Ok(user);
    }
    [HttpPost("refresh")]
    public IActionResult Refresh([FromBody] string refreshToken)
    {
        var newTokens = tokenService.GetNewTokensFromRefreshToken(refreshToken);
        return Ok(new{NewAccessToken = newTokens.accessToken, newRefreshToken = newTokens.refreshToken});
    }

}