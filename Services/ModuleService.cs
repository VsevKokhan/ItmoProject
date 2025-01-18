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

    public async Task<Module?> GetModule(string nameOfModule)
    {
        return await context.Modules.AsNoTracking().FirstOrDefaultAsync(m => m.Name == nameOfModule);
    }

    public async Task<bool> MakeModuleCompleted(int idModule, int idUser)
    {
        var um = await context.UserModules.FirstOrDefaultAsync(um => um.User_Id == idUser && um.Module_Id == idModule);
        if (um == null)
        {
            return false;
        }
        um.Is_Passed = true;

        await context.SaveChangesAsync();
        return true;
    }
}