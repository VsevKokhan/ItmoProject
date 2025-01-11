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
    public AuthController(IUserService serv, TokenService tokenService)
    {
        service = serv;
        this.tokenService = tokenService;
    }
    
    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserDto user)
    {
        var newUser = service.Add(user);
        
        var token = tokenService.GenerateToken(newUser.Id);

        return Ok(token);
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] UserForLogin user)
    {
        var name = user.Name;
        var pas = user.Pass;
        var userEntity = service.Get(pas, name);
        var tok = tokenService.GenerateToken(userEntity.Id);

        return Ok(tok);
    }
    [HttpPost("GetUserByToken")]
    public IActionResult Get([FromBody] string token)
    {
        var id = tokenService.GetIdFromToken(token);
        
        var user = service.Get(id);

        return Ok(user);
    }

}