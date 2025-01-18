using Interfaces;
using Microsoft.AspNetCore.Authorization;
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
    
    [Authorize]
    [HttpGet("GetCourses")]
    public IActionResult Get()
    {
        var courses = courseService.GetCourses();
        
        return Ok(courses);
    }
    [Authorize]
    [HttpGet("GetModulesOfCourse")]
    public IActionResult GetModulesOfCourse(string nameOfCourse)
    {
        var modules = courseService.GetModulesOfCourse(nameOfCourse);
        
        return Ok(modules);
    }


}