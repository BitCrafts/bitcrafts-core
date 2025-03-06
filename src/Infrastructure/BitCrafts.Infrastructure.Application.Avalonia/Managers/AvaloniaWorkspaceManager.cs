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
    private Dictionary<IPresenter, TabItem> _presentersToTabItemMap = new Dictionary<IPresenter, TabItem>();

    public AvaloniaWorkspaceManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetTabControl(TabControl tabControl)
    {
        _tabControl = tabControl;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public void ShowPresenter(IPresenter presenter)
    {
        if (_presentersToTabItemMap.ContainsKey(presenter))
            return;
        var view = presenter.GetView();
        var tabItem = new TabItem { Header = view.GetTitle(), Content = (UserControl)view };
        if (_tabControl.Items.Add(tabItem) != -1)
        {
            _presentersToTabItemMap.TryAdd(presenter, tabItem);
        }
    }

    public void ClosePresenter(IPresenter presenter)
    {
        if (_presentersToTabItemMap.TryGetValue(presenter, out TabItem tabItem))
        {
            _tabControl.Items.Remove(tabItem);
            _presentersToTabItemMap.Remove(presenter);
            presenter.Dispose();
        }
    }
}