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
    public AuthController(IUserService serv)
    {
        service = serv;
    }
    
    [HttpPost("register")]
    public IActionResult Add([FromBody] UserDto user)
    {
        service.Add(user);
        return Ok();
    }
    [HttpPost("GetUser/{id}")]
    public IActionResult Get([FromRoute]int id)
    {
        var serv = (UserService)service;
        var s = serv.Get(id);
        return Ok(s);
    }

}