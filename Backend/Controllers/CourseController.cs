using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CourseController : ControllerBase
{
    private CourseService courseService;
    public CourseController(CourseService moduleService)
    {
        this.courseService = moduleService;
    }
    
    [HttpPost("GetCourses")]
    public IActionResult Get()
    {
        var s = courseService.GetCourses();
        
        return Ok(s);
    }

}