using Data;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Services;

public class CourseService
{
    private AppDbContext context;
    public CourseService(AppDbContext context)
    {
        this.context = context;
    }
    public IEnumerable<string> GetCourses()
    {
        return context.Courses.AsNoTracking().Select(c => c.Name);
    }

}