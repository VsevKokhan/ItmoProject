using Models.Model;

namespace Interfaces;

public interface IModuleService
{
    public Module Get(string nameOfModule);
    public IEnumerable<Module> GetModulesOfCourse(string nameOfCourse);
}