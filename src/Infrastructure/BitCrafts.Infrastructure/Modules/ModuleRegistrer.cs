using System.Reflection;
using System.Runtime.Loader;
using BitCrafts.Infrastructure.Abstraction.Modules;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Infrastructure.Modules;

public sealed class ModuleRegistrer : IModuleRegistrer
{
    private readonly ILogger _logger;
    private List<Assembly> _loadedAssemblies;

    public ModuleRegistrer(ILogger logger)
    {
        _logger = logger;
        _loadedAssemblies = new List<Assembly>();
    }

    public void RegisterModules(IServiceCollection services)
    {
        var modulesPath = GetModulesPath();
        if (string.IsNullOrEmpty(modulesPath)) return;
        var allFiles = Directory.GetFiles(modulesPath, "*.dll");

        foreach (var dll in allFiles.Where(x => x.Contains("Abstraction")))
        {
            var dllName = Path.GetFileName(dll);
            _logger.Information($"Loading assembly {dllName}...");
            LoadAssembly(dll, services);
            _logger.Information($"Loaded assembly {dllName} OK.");
        }

        foreach (var dll in allFiles.Where(x => !x.Contains("Abstraction")))
        {
            var dllName = Path.GetFileName(dll);
            _logger.Information($"Loading assembly {dllName}...");
            LoadAssembly(dll, services, true);
            _logger.Information($"Loaded assembly {dllName} OK.");
        }
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

    private void LoadAssembly(string dll, IServiceCollection services, bool registerAsModule = false)
    {
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
        _loadedAssemblies.Add(assembly);
        if (registerAsModule) RegisterModules(assembly, services);
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
                    services.AddSingleton(moduleInstance);
                }
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error loading assembly {assembly.FullName}");
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