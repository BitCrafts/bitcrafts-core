using System;
using System.Collections.Generic;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWorkspaceManager : IWorkspaceManager
{
    private readonly IServiceProvider _serviceProvider;
    private TabControl _tabControl;
    private Dictionary<string, dynamic> _views = new();

    public AvaloniaWorkspaceManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetControl(TabControl tabControl)
    {
        _tabControl = tabControl;
    }

    public async void ShowPresenter(string title, dynamic presenter)
    {
        var added = _views.TryAdd(title, presenter);
        if (!added) return;

        var tabItem = new TabItem { Header = title, Content = presenter.View };
        _tabControl.Items.Add(tabItem);
        await presenter.InitializeAsync();
        presenter.Show();
    }

    public void ClosePresenter(string title)
    {
        var content = _views.TryGetValue(title, out var view) ? view : null;
        if (content == null) return;
        foreach (TabItem tabItem in _tabControl.Items)
        {
            if (Equals(tabItem.Content, content.View))
            {
                _tabControl.Items.Remove(tabItem);
                break;
            }
        }
    }
}