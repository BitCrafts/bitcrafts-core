using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWorkspaceManager : IWorkspaceManager
{
    private readonly IServiceProvider _serviceProvider;
    private TabControl _tabControl;
    private Dictionary<Type, dynamic> _views = new();

    public AvaloniaWorkspaceManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetControl(TabControl tabControl)
    {
        _tabControl = tabControl;
    }


    public async Task ShowPresenterAsync(dynamic presenter)
    {
        var added = _views.TryAdd(presenter.GetType(), presenter);
        if (!added) return;

        var tabItem = new TabItem { Header = presenter.Title, Content = presenter.View };
        _tabControl.Items.Add(tabItem);
        await presenter.ShowAsync();
    }

    public async Task ClosePresenterAsync(Type presenterType)
    {
        var content = _views.TryGetValue(presenterType, out var view) ? view : null;
        if (content == null) return;
        foreach (TabItem tabItem in _tabControl.Items)
        {
            if (Equals(tabItem.Content, content.View))
            {
                await content.CloseAsync();
                content.Dispose();
                _tabControl.Items.Remove(tabItem);
                break;
            }
        }
    }
}