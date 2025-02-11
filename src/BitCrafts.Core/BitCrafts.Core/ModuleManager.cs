using System.Reflection;
using System.Runtime.Loader;
using BitCrafts.Core.Contracts;
using BitCrafts.Core.Contracts.Modules;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace BitCrafts.Core;

public class ModuleManager : IModuleManager
{
    private readonly IIoCRegister _ioCContainer;
    private readonly IConfiguration _configuration;
    private readonly ILogger _logger;
    private readonly List<Assembly> _loadedAssemblies;

    private readonly Dictionary<string,
        (Type ViewContract, Type ViewImplementation,
        Type PresenterContract, Type PresenterImplementation,
        Type ModelType)> _moduleTypeRegistry
        = new();


    public ModuleManager(IIoCRegister ioCContainer, IConfiguration configuration, ILogger logger)
    {
        _ioCContainer = ioCContainer;
        _configuration = configuration;
        _logger = logger;
        _loadedAssemblies = new List<Assembly>();
    }

    public IReadOnlyDictionary<string,
        (Type ViewContract, Type ViewImplementation,
        Type PresenterContract, Type PresenterImplementation,
        Type ModelType)> GetModuleViewTypes()
    {
        return _moduleTypeRegistry;
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

    private string GetModulesPath()
    {
        var modulesPath = _configuration["ModulesPath"];
        if (string.IsNullOrEmpty(modulesPath))
        {
            _logger.Warning("ModulesPath is not configured. No modules will be loaded.");
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
            _logger.Information($"Successfully loaded assembly: {assembly.FullName}");
        }
        catch (Exception ex)
        {
            _logger.Warning($"Failed to load assembly {dll}: {ex.Message}");
        }
    }

    private void RegisterModules(Assembly assembly)
    {
        var moduleTypes = assembly.GetTypes().Where(IsValidModule);
        foreach (var type in moduleTypes)
        {
            _logger.Information($"Found module: {type.FullName}");
            if (Activator.CreateInstance(type) is IModule moduleInstance)
            {
                moduleInstance.RegisterServices(_ioCContainer);
                var (viewContract, viewImplementation) = moduleInstance.GetViewType();
                var (presenterContract, presenterImplementation) = moduleInstance.GetPresenterType();
                var modelType = moduleInstance.GetModelType();

                if (!_moduleTypeRegistry.TryAdd(moduleInstance.Name,
                        (viewContract, viewImplementation,
                            presenterContract, presenterImplementation,
                            modelType)))
                {
                    _logger.Warning(
                        $"Impossible d’enregistrer le module '{moduleInstance.Name}', " +
                        $"car il est déjà présent. " +
                        $"Types détectés : " +
                        $"ViewContract = {viewContract?.FullName}, " +
                        $"ViewImplementation = {viewImplementation?.FullName}, " +
                        $"PresenterContract = {presenterContract?.FullName}, " +
                        $"PresenterImplementation = {presenterImplementation?.FullName}, " +
                        $"ModelType = {modelType?.FullName}"
                    );
                }


                _logger.Information($"Module {moduleInstance.Name} of type {type.FullName} configured successfully.");
            }
        }
    }

    private bool IsValidClass(Type type) => type.IsClass && !type.IsAbstract;
    private bool IsValidModule(Type type) => IsValidClass(type) && typeof(IModule).IsAssignableFrom(type);
}