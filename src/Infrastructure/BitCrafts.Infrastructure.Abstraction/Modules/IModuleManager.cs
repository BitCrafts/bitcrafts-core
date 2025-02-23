namespace BitCrafts.Infrastructure.Abstraction.Modules;

public interface IModuleManager
{
    IReadOnlyDictionary<string, IModule> Modules { get; }
    void AddModule(IModule module);
    void AddModules(IEnumerable<IModule> modules);
}