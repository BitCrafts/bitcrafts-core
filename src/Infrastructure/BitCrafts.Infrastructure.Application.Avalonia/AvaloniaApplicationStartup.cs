using System;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Application.Avalonia.Presenters;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public class AvaloniaApplicationStartup : IApplicationStartup
{
    private IPresenter<IStartupView> _startupPresenter;

    public AvaloniaApplicationStartup(IPresenter<IStartupView> startupPresenter)
    {
        _startupPresenter = startupPresenter;
    }

    public void Dispose()
    {
        _startupPresenter.Dispose();
    }

    public Task StartAsync()
    {
        _startupPresenter.Initialize();
        _startupPresenter.Show();
        return Task.CompletedTask;
    }
}