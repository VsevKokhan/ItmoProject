using Models.Model;

namespace Interfaces;

public interface IModuleService
{
    public Task<Module?> GetModule(string nameOfModule);
    public Task<bool> MakeModuleCompleted(int idModule, int idUser);
}