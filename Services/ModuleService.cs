using Data;
using Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.Model;

namespace Services;

public class ModuleService : IModuleService
{
    private AppDbContext context;
    public ModuleService(AppDbContext context)
    {
        this.context = context;
    }
    public Module Get(string nameOfModule)
    {
        return context.Modules.AsNoTracking().First(m => m.Name == nameOfModule);
    }

    public IEnumerable<Module> GetModulesOfCourse(string nameOfCourse)
    {
        return context.Modules.AsNoTracking().Include(m => m.Course).Where(m => m.Course.Name == nameOfCourse);
    }
}