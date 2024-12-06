using System.Security.Claims;
using System.Text;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private IUserService service;
    public AuthController(IUserService serv)
    {
        service = serv;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] string s)
    {
        return Ok(new {A = "JWTTOKEN", B = "RefreshToken"});
    }


    [HttpPost("login")]
    public IActionResult Login()
    {
        return Ok();
    }

    [HttpPost("refresh")]
    public IActionResult Refresh()
    {
        return Ok();
    }
    [HttpPost("add")]
    public IActionResult Add([FromBody] UserDto user)
    {
        service.Add(user);
        return Ok();
    }

}