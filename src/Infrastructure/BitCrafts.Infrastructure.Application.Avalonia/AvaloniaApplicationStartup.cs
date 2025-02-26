using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public class AvaloniaApplicationStartup : IApplicationStartup
{
    private IPresenter<IStartupView> _startupPresenter;
    private readonly IModuleManager _moduleManager;

    public AvaloniaApplicationStartup(IStartupPresenter startupPresenter, IModuleManager moduleManager)
    {
        _startupPresenter = startupPresenter;
        _moduleManager = moduleManager;
    }

    public void Dispose()
    {
        _startupPresenter.Dispose();
    }

    public async Task StartAsync()
    {
        await _startupPresenter.InitializeAsync();
        _startupPresenter.Show(); 
    }
}