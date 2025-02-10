namespace BitCrafts.Core.Contracts;

public interface IModuleManager
{
    void LoadModules(); 

    IReadOnlyDictionary<string,
        (Type ViewContract, Type ViewImplementation,
        Type PresenterContract, Type PresenterImplementation,
        Type ModelType)> GetModuleViewTypes();
}