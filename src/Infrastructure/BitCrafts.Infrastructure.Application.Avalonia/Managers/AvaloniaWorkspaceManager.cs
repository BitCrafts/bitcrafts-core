using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWorkspaceManager : IWorkspaceManager
{
    private readonly ILogger<AvaloniaWorkspaceManager> _logger;
    private readonly Dictionary<IPresenter, TabItem> _presenterToTabItemMap;
    private readonly IServiceProvider _serviceProvider;
    private TabControl _tabControl;

    public AvaloniaWorkspaceManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _presenterToTabItemMap = new Dictionary<IPresenter, TabItem>();
        _logger = _serviceProvider.GetRequiredService<ILogger<AvaloniaWorkspaceManager>>();
    }


    public void ShowPresenter<TPresenter>() where TPresenter : class, IPresenter
    {
        ShowPresenter(typeof(TPresenter));
    }

    public void ShowPresenter(Type presenterType)
    {
        if (presenterType == null) return;
        var existingPresenter = GetExistingPresenter(presenterType);
        if (existingPresenter != null)
        {
            if (_presenterToTabItemMap.TryGetValue(existingPresenter, out var existingTabItem))
                _tabControl.SelectedItem = existingTabItem;

            return;
        }

        var presenter = _serviceProvider.GetRequiredService(presenterType) as IPresenter;
        if (presenter != null)
        {
            var view = presenter.GetView() as UserControl;
            if (view == null)
                throw new InvalidOperationException("The view associated with the presenter is not a UserControl.");
            var closePresenterButton = new Button()
            {
                Content = "X", BorderThickness = new Thickness(0), CornerRadius = new CornerRadius(0),
                HorizontalAlignment = HorizontalAlignment.Right
            };
            var titleHeader = new TextBlock()
            {
                Text = ((IView)view).Title,
                VerticalAlignment = VerticalAlignment.Center
            };
            closePresenterButton.Click += (_, _) => ClosePresenterByInstance(presenter);
            Grid.SetColumn(titleHeader, 0);
            Grid.SetColumn(closePresenterButton, 1);
            var gridHeader = new Grid()
            {
                MinWidth = 150,
                ColumnDefinitions = new ColumnDefinitions()
                {
                    new ColumnDefinition(GridLength.Star),
                    new ColumnDefinition(GridLength.Auto)
                },
                Children =
                {
                    titleHeader,
                    closePresenterButton
                }
            };
            var tabItem = new TabItem { Header = gridHeader, Content = view };

            _presenterToTabItemMap[presenter] = tabItem;
            _tabControl.Items.Add(tabItem);
            _tabControl.SelectedItem = tabItem;
        }
    }

    public void ClosePresenter(Type presenterType)
    {
        var presenter = GetExistingPresenter(presenterType);
        if (presenter == null) return;

        if (_presenterToTabItemMap.TryGetValue(presenter, out var tabItem))
        {
            _tabControl.Items.Remove(tabItem);
            _presenterToTabItemMap.Remove(presenter);
            if (presenter is IDisposable disposablePresenter) disposablePresenter.Dispose();
        }
    }

    public void ClosePresenter<TPresenter>() where TPresenter : class, IPresenter
    {
        ClosePresenter(typeof(TPresenter));
    }

    public void Dispose()
    {
        _logger.LogInformation("Disposing AvaloniaWorkspaceManager...");
        var presenters = _presenterToTabItemMap.Keys.ToArray();
        foreach (var presenter in presenters) ClosePresenterByInstance(presenter);

        _presenterToTabItemMap.Clear();

        _logger.LogInformation("Disposed AvaloniaWorkspaceManager.");
    }

    public void SetTabControl(TabControl tabControl)
    {
        _tabControl = tabControl ?? throw new ArgumentNullException(nameof(tabControl));
    }

    private IPresenter GetExistingPresenter(Type presenterType)
    {
        foreach (var (presenter, _) in _presenterToTabItemMap)
        {
            var interfaces = presenter.GetType().GetInterfaces();
            if (interfaces.Contains(presenterType) || presenter.GetType() == presenterType) return presenter;
        }

        return null;
    }

    private void ClosePresenterByInstance(IPresenter presenter)
    {
        if (_presenterToTabItemMap.TryGetValue(presenter, out var tabItem))
        {
            _tabControl.Items.Remove(tabItem);
            _presenterToTabItemMap.Remove(presenter);
            if (presenter is IDisposable disposablePresenter) disposablePresenter.Dispose();
        }
    }
}