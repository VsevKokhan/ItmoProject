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
    public IEnumerable<Course> GetCourses()
    {
        return context.Courses.AsNoTracking();
    }
    public IEnumerable<Module> GetModulesOfCourse(string nameOfCourse)
    {
        return context.Modules.AsNoTracking().Where(m => m.Course.Name == nameOfCourse);
    }

}