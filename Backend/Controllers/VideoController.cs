using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VideoController : ControllerBase
{
    
    [HttpGet("Video/{video}")]
     public IActionResult GetVideo([FromRoute] string video)
     {
         var firstPath = @"C:\Users\sevak\OneDrive\Рабочий стол\Pen\";
         var filePath = @$"{firstPath}ItmoProject\videos\{video}.mp4";
         var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
         return File(fileStream, "video/mp4", "video.mp4");
     }
}