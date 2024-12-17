using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;
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
        
        service.Add(user);
        
        var servi = service as UserService;

        var name = user.Name;
        var pas = user.Password_HK;
        var useri = servi.Get(pas, name);
        var token = tokenService.GenerateToken(useri.Id);

        return Ok(token);
    }
    [HttpPost("Login")]
    public IActionResult Login([FromBody] USser user)
    {
        var servi = service as UserService;

        var name = user.Name;
        var pas = user.Pass;
        var useri = servi.Get(pas, name);
        var tok = tokenService.GenerateToken(useri.Id);

        return Ok(tok);
    }
    [HttpPost("GetUserByToken")]
    public IActionResult Get([FromBody] string token)
    {
        var servi = service as UserService;

        var id = tokenService.GetIdFromToken(token);
        

        var user = servi.Get(int.Parse(id));

        return Ok(user);
    }

}

public class USser
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Pass { get; set; }
}