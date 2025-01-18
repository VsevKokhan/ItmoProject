using System.Security.Claims;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models.DTO;
using Models.Model;
using Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModuleController : ControllerBase
{
    private IModuleService moduleService;
    public ModuleController(IModuleService moduleService)
    {
        this.moduleService = moduleService;
    }
    
    [Authorize]
    [HttpPost("MakeModuleCompleted")]
    public async Task<IActionResult> MakeModuleCompleted([FromBody] string nameOfModule)
    {
        var idModule = (await moduleService.GetModule(nameOfModule))?.Id;
        if (idModule == null)
        {
            return NotFound(new {message = "Module not found"});
        }
        var IsCompleted= await moduleService.MakeModuleCompleted((int)idModule, int.Parse(User.FindFirstValue("id")));
        if (!IsCompleted)
        {
            return BadRequest(new {message = "internal exception"});
        }
        return Ok(new {mesage = "Module completed"});
    }
    [Authorize]
    [HttpPost("GetModule")]
    public async Task<IActionResult> GetModule([FromBody] string nameOfModule)
    {
        var s = await moduleService.GetModule(nameOfModule);
        if (s == null)
        {
            return NotFound(new {message = "Module not found"});
        }
        
        return Ok(s);
    }

}
