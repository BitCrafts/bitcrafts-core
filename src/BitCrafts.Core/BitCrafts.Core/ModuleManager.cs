using System.Reflection;
using System.Runtime.Loader;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core;

public sealed class ModuleManager : IModuleManager
{
    private List<Assembly> _loadedAssemblies;

    private readonly Dictionary<string,
        (Type ViewContract, Type ViewImplementation,
        Type PresenterContract, Type PresenterImplementation,
        Type ModelType)> _moduleTypeRegistry
        = new();


    public ModuleManager()
    {
        _loadedAssemblies = new List<Assembly>();
    }

    public IReadOnlyDictionary<string,
        (Type ViewContract, Type ViewImplementation,
        Type PresenterContract, Type PresenterImplementation,
        Type ModelType)> GetModuleViewTypes()
    {
        return _moduleTypeRegistry;
    }

    public void LoadModules(IServiceCollection services)
    {
        var modulesPath = GetModulesPath();
        if (string.IsNullOrEmpty(modulesPath)) return;

        foreach (var dll in Directory.GetFiles(modulesPath, "*.dll"))
            LoadAssembly(dll, services);
    }

    private string GetModulesPath()
    {
        var modulesPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
        if (string.IsNullOrEmpty(modulesPath))
        {
            return null;
        }

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
        var moduleTypes = assembly.GetTypes().Where(IsValidModule);
        foreach (var type in moduleTypes)
        {
            if (Activator.CreateInstance(type) is IModule moduleInstance)
            {
                moduleInstance.RegisterServices(services);
                var (viewContract, viewImplementation) = moduleInstance.GetViewType();
                var (presenterContract, presenterImplementation) = moduleInstance.GetPresenterType();
                var modelType = moduleInstance.GetModelType();

                if (!_moduleTypeRegistry.TryAdd(moduleInstance.Name,
                        (viewContract, viewImplementation,
                            presenterContract, presenterImplementation,
                            modelType)))
                {
                    throw new InvalidOperationException(
                        $"Module {moduleInstance.Name} of type {type.FullName} already registered.");
                }
            }
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

    public void Dispose()
    {
        _loadedAssemblies.Clear();
        _loadedAssemblies = null;
    }
}