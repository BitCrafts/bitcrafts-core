using BitCrafts.Infrastructure.Abstraction.Application.UI;

namespace BitCrafts.Infrastructure.Abstraction.Application;

public interface IUiManager : IDisposable
{
    Task StartAsync();  
}