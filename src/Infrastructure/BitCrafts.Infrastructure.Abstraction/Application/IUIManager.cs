namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IUiManager : IDisposable
{
    Task StartAsync();  
}