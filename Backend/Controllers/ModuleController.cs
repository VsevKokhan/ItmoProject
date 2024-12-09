using Interfaces;
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
    
    [HttpPost("GetModules")]
    public IActionResult Get([FromBody] string nameOfCourse)
    {
        var s = moduleService.GetModulesOfCourse(nameOfCourse);
        
        return Ok(s);
    }

}
