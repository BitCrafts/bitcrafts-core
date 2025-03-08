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
    private readonly Dictionary<IPresenter, IServiceScope> _presenterToScopeMap = new();

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
        var scope = _serviceProvider.CreateScope();
        var presenter = scope.ServiceProvider.GetRequiredService(presenterType) as IPresenter;
        if (_presenterToTabItemMap.ContainsKey(presenter))
        {
            _tabControl.SelectedItem = _presenterToTabItemMap[presenter];
            return;
        }

        var view = presenter.GetView() as UserControl; // Cast en UserControl
        if (view == null)
        {
            scope.Dispose();
            throw new InvalidOperationException("The view associated with the presenter is not a UserControl.");
        }

        var tabItem = new TabItem { Header = ((IView)view).GetTitle(), Content = view };

        _presenterToTabItemMap[presenter] = tabItem;
        _presenterToScopeMap[presenter] = scope;
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


            if (_presenterToScopeMap.TryGetValue(presenter, out var scopeToDispose))
            {
                scopeToDispose.Dispose();
                _presenterToScopeMap.Remove(presenter);
            }

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
        foreach (var scope in _presenterToScopeMap.Values)
        {
            var presenter = scope.ServiceProvider.GetService(presenterType) as IPresenter;
            if (presenter != null)
            {
                return presenter;
            }
        }

        return default;
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing AvaloniaWorkspaceManager...");
        var presenters = _presenterToTabItemMap.Keys.ToArray();
        foreach (var presenter in presenters)
        {
            ClosePresenterByInstance(presenter);
        }

        foreach (var scope in _presenterToScopeMap.Values)
        {
            scope.Dispose();
        }

        _presenterToScopeMap.Clear();
        _presenterToTabItemMap.Clear();

        _logger.LogInformation("Disposed AvaloniaWorkspaceManager.");
    }

    private void ClosePresenterByInstance(IPresenter presenter)
    {
        if (_presenterToTabItemMap.TryGetValue(presenter, out TabItem tabItem))
        {
            _tabControl.Items.Remove(tabItem);
            _presenterToTabItemMap.Remove(presenter);

            if (_presenterToScopeMap.TryGetValue(presenter, out var scopeToDispose))
            {
                scopeToDispose.Dispose();
                _presenterToScopeMap.Remove(presenter);
            }

            if (presenter is IDisposable disposablePresenter)
            {
                disposablePresenter.Dispose();
            }
        }
    }
}