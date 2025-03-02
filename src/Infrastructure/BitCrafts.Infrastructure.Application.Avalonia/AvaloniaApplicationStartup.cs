using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public class AvaloniaApplicationStartup : IApplicationStartup
{
    private IPresenter<IStartupView> _startupPresenter;

    public AvaloniaApplicationStartup(IStartupPresenter startupPresenter)
    {
        _startupPresenter = startupPresenter;
    }

    public void Dispose()
    {
        _startupPresenter.Dispose();
    }

    public async Task StartAsync()
    {
       await _startupPresenter.ShowAsync(); 
    }
}