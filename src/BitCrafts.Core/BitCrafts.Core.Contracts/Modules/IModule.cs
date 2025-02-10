namespace BitCrafts.Core.Contracts.Modules;

public interface IModule
{
    string Name { get; }
    void RegisterServices(IIoCRegister ioCContainer);

    (Type viewContract, Type viewImplementation) GetViewType();

    (Type presenterContract, Type presenterImplementation) GetPresenterType();

    Type GetModelType();
}