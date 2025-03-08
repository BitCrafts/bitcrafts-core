using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly Dictionary<IPresenter, TabItem> _presenterToTabItemMap = new();

    private ILogger<AvaloniaWorkspaceManager> _logger;

    public AvaloniaWorkspaceManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logger = _serviceProvider.GetRequiredService<ILogger<AvaloniaWorkspaceManager>>();
    }

    public void SetTabControl(TabControl tabControl)
    {
        _tabControl = tabControl ?? throw new ArgumentNullException(nameof(tabControl));
    }


    public void ShowPresenter<TPresenter>() where TPresenter : IPresenter
    {
        ShowPresenter(typeof(TPresenter));
    }

    public void ShowPresenter(Type presenterType)
    {
        var presenter = _serviceProvider.GetRequiredService(presenterType) as IPresenter;
        if (_presenterToTabItemMap.ContainsKey(presenter))
        {
            _tabControl.SelectedItem = _presenterToTabItemMap[presenter];
            return;
        }

        var view = presenter.GetView() as UserControl; // Cast en UserControl
        if (view == null)
        {
            throw new InvalidOperationException("The view associated with the presenter is not a UserControl.");
        }

        var tabItem = new TabItem { Header = ((IView)view).GetTitle(), Content = view };

        _presenterToTabItemMap[presenter] = tabItem;
        _tabControl.Items.Add(tabItem);
        _tabControl.SelectedItem = tabItem;
    }

    public void ClosePresenter(Type presenterType)
    {
        var presenter = GetPresenterFromAnyScope(presenterType);
        if (presenter == null) return;

        if (_presenterToTabItemMap.TryGetValue(presenter, out TabItem tabItem))
        {
            _tabControl.Items.Remove(tabItem);
            _presenterToTabItemMap.Remove(presenter);


            if (presenter is IDisposable disposablePresenter)
            {
                disposablePresenter.Dispose();
            }
        }
    }

    public void ClosePresenter<TPresenter>() where TPresenter : IPresenter
    {
        ClosePresenter(typeof(TPresenter));
    }

    private IPresenter GetPresenterFromAnyScope(Type presenterType)
    {
        var presenter = _serviceProvider.GetService(presenterType) as IPresenter;

        return presenter;
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing AvaloniaWorkspaceManager...");
        var presenters = _presenterToTabItemMap.Keys.ToArray();
        foreach (var presenter in presenters)
        {
            ClosePresenterByInstance(presenter);
        }

        _presenterToTabItemMap.Clear();

        _logger.LogInformation("Disposed AvaloniaWorkspaceManager.");
    }

    private void ClosePresenterByInstance(IPresenter presenter)
    {
        if (_presenterToTabItemMap.TryGetValue(presenter, out TabItem tabItem))
        {
            _tabControl.Items.Remove(tabItem);
            _presenterToTabItemMap.Remove(presenter);

            if (presenter is IDisposable disposablePresenter)
            {
                disposablePresenter.Dispose();
            }
        }
    }
}