using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Modules;

public sealed class ModuleManager : IModuleManager
{
    private readonly Dictionary<string, IModule> _modules = new();
    public IReadOnlyDictionary<string, IModule> Modules => _modules;

    public void AddModule(IModule module)
    {
        if (!_modules.ContainsKey(module.Name))
            _modules.Add(module.Name, module);
        else
            throw new Exception($"Un module avec le nom '{module.Name}' existe déjà.");
    }

    public void AddModules(IEnumerable<IModule> modules)
    {
        foreach (var module in modules) AddModule(module);
    }

    public IModule GetModuleByName(string name)
    {
        return _modules.TryGetValue(name, out var module) ? module : null;
    }
}