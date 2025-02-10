using System.Reflection;
using System.Runtime.Loader;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace BitCrafts.Core;

public class ModuleManager : IModuleManager
{
    private readonly IIoCContainer _ioCContainer;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly List<Assembly> _loadedAssemblies;
    private readonly Dictionary<Type, List<Type>> _interfaceImplementations;
    private readonly Type[] _applicationInterfaces = { typeof(IGtkApplication), typeof(IConsoleApplication) };

    public ModuleManager(IIoCContainer ioCContainer, IConfiguration configuration, ILogger logger)
    {
        _ioCContainer = ioCContainer;
        _configuration = configuration;
        _logger = logger;
        _loadedAssemblies = new List<Assembly>();
        _interfaceImplementations = new Dictionary<Type, List<Type>>();
    }

    public void LoadModules()
    {
        string modulesPath = GetModulesPath();
        if (string.IsNullOrEmpty(modulesPath)) return;

        foreach (var dll in Directory.GetFiles(modulesPath, "*.dll"))
        {
            LoadAssembly(dll);
        }
    }

    public IEnumerable<Type> GetImplementationsOf<T>() where T : class
    {
        return _interfaceImplementations.TryGetValue(typeof(T), out var implementations)
            ? implementations
            : Enumerable.Empty<Type>();
    }

    private string GetModulesPath()
    {
        var modulesPath = _configuration["ModulesPath"];
        if (string.IsNullOrEmpty(modulesPath))
        {
            _logger.Warning("ModulesPath is not configured. No modules will be loaded.");
            return null;
        }

        if (!Directory.Exists(modulesPath))
        {
            _logger.Warning($"Modules directory '{modulesPath}' does not exist. Creating it...");
            Directory.CreateDirectory(modulesPath);
            return null;
        }

        return Path.IsPathRooted(modulesPath) ? modulesPath : Path.GetFullPath(modulesPath);
    }

    private void LoadAssembly(string dll)
    {
        try
        {
            var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
            _loadedAssemblies.Add(assembly);

            RegisterModules(assembly);
            RegisterApplicationInterfaces(assembly);
            IndexInterfaces(assembly);

            _logger.Information($"Successfully loaded assembly: {assembly.FullName}");
        }
        catch (Exception ex)
        {
            _logger.Warning($"Failed to load assembly {dll}: {ex.Message}");
        }
    }

    private void RegisterModules(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(IsValidModule))
        {
            _logger.Information($"Found module: {type.FullName}");
            if (Activator.CreateInstance(type) is IModule moduleInstance)
            {
                moduleInstance.RegisterServices(_ioCContainer);
                _logger.Information($"Module {moduleInstance.Name} of type {type.FullName} configured successfully.");
            }
        }
    }

    private void RegisterApplicationInterfaces(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(t => IsValidClass(t) && ImplementsAnyApplicationInterface(t)))
        {
            foreach (var appInterface in _applicationInterfaces.Where(appInterface =>
                         appInterface.IsAssignableFrom(type)))
            {
                _logger.Information($"Found implementation of {appInterface.Name}: {type.FullName}");
                _ioCContainer.Register(appInterface, type, ServiceLifetime.Singleton);
                _logger.Information($"Service {type.FullName} injected into IoC for {appInterface.Name}");
            }
        }
    }

    private void IndexInterfaces(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes().Where(IsValidClass))
        {
            foreach (var implementedInterface in type.GetInterfaces())
            {
                if (!_interfaceImplementations.ContainsKey(implementedInterface))
                {
                    _interfaceImplementations[implementedInterface] = new List<Type>();
                }

                _interfaceImplementations[implementedInterface].Add(type);
            }
        }
    }

    private bool IsValidClass(Type type) => type.IsClass && !type.IsAbstract;
    private bool IsValidModule(Type type) => IsValidClass(type) && typeof(IModule).IsAssignableFrom(type);

    private bool ImplementsAnyApplicationInterface(Type type) =>
        _applicationInterfaces.Any(i => i.IsAssignableFrom(type));
}