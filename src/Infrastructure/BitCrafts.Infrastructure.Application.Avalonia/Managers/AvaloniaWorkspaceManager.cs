using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWorkspaceManager : IWorkspaceManager
{
    private readonly IServiceProvider _serviceProvider;
    private TabControl _tabControl;
    private Dictionary<Type, IPresenter> _views = new();

    public AvaloniaWorkspaceManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetControl(TabControl tabControl)
    {
        _tabControl = tabControl;
    }

    public async Task ShowPresenterAsync(IPresenter presenter)
    {
        var added = _views.TryAdd(presenter.GetType(), presenter);
        if (!added) return;
        var view = presenter.GetView<IView>(); 
        var tabItem = new TabItem { Header = view.GetTitle(), Content = view };
        _tabControl.Items.Add(tabItem);
        await presenter.ShowAsync();
    }

    public async Task ClosePresenterAsync(IPresenter presenter)
    {
        var content = _views.TryGetValue(presenter.GetType(), out var view) ? view : null;
        if (content == null) return;
        foreach (TabItem tabItem in _tabControl.Items)
        {
            if (Equals(tabItem.Content, content.GetView<IView>()))
            {
                await content.CloseAsync();
                content.Dispose();
                _tabControl.Items.Remove(tabItem);
                break;
            }
        }
    }

    public void Dispose()
    {
        foreach (var (presenterType, presenter) in _views)
        {
            _serviceProvider.GetRequiredService<ILogger<AvaloniaWorkspaceManager>>()
                .LogInformation($"Disposing {presenterType} Presenter");
        }
    }
}