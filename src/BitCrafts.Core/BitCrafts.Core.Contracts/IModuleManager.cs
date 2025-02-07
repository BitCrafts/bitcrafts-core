namespace BitCrafts.Core.Contracts;

public interface IModuleManager
{
    void LoadModules();
    IEnumerable<Type> GetImplementationsOf<T>() where T : class;
}