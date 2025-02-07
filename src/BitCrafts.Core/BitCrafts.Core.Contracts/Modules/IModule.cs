namespace BitCrafts.Core.Contracts.Modules;

public interface IModule
{
    void RegisterServices(IIoCContainer ioCContainer);
}