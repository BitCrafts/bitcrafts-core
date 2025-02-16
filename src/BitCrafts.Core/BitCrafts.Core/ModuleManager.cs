using System.Reflection;
using System.Runtime.Loader;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core;

public sealed class ModuleManager : IModuleManager
{
    private List<Assembly> _loadedAssemblies;

    public ModuleManager()
    {
        _loadedAssemblies = new List<Assembly>();
    }

    public void LoadModules(IServiceCollection services)
    {
        var modulesPath = GetModulesPath();
        if (string.IsNullOrEmpty(modulesPath)) return;

        foreach (var dll in Directory.GetFiles(modulesPath, "*.dll"))
            LoadAssembly(dll, services);
    }

    public void Dispose()
    {
        _loadedAssemblies.Clear();
        _loadedAssemblies = null;
    }

    private string GetModulesPath()
    {
        var modulesPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
        if (string.IsNullOrEmpty(modulesPath)) return null;

        return Path.IsPathRooted(modulesPath) ? modulesPath : Path.GetFullPath(modulesPath);
    }

    private void LoadAssembly(string dll, IServiceCollection services)
    {
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
        _loadedAssemblies.Add(assembly);
        RegisterModules(assembly, services);
    }

    private void RegisterModules(Assembly assembly, IServiceCollection services)
    {
        try
        {
            var moduleTypes = assembly.GetTypes().Where(IsValidModule);
            foreach (var type in moduleTypes)
                if (Activator.CreateInstance(type) is IModule moduleInstance)
                {
                    moduleInstance.RegisterServices(services);
                }
        }
        catch (Exception ex)
        {
            Log.Error(ex, $"Erreur lors de l’enregistrement des modules dans l’assembly {assembly.FullName}");
        }
    }

    private bool IsValidClass(Type type)
    {
        return type.IsClass && !type.IsAbstract;
    }

    private bool IsValidModule(Type type)
    {
        return IsValidClass(type) && typeof(IModule).IsAssignableFrom(type);
    }
}