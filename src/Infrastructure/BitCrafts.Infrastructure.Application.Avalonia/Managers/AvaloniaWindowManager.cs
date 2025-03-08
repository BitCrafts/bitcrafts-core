using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using Avalonia.Media;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWindowManager : IWindowManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Stack<Window> _windowStack = new();
    private readonly Dictionary<IPresenter, Window> _presenterToWindowMap = new();
    private readonly Dictionary<IPresenter, IServiceScope> _presenterToScopeMap = new(); 
    private Window _activeWindow;

    public AvaloniaWindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowWindow<TPresenter>() where TPresenter : IPresenter
    {
        Type presenterType = typeof(TPresenter);
        var scope = _serviceProvider.CreateScope();
        var presenter = scope.ServiceProvider.GetRequiredService<TPresenter>();
        if (_presenterToWindowMap.ContainsKey(presenter))
        {
            HandleExistingWindow(_presenterToWindowMap[presenter]);
            return;
        }

        var view = presenter.GetView() as UserControl;
        if (view == null)
        {
            scope.Dispose();
            throw new InvalidOperationException("The view associated with the presenter is not a UserControl.");
        }

        var window = CreateWindow(view, ((IView)view).GetTitle());
        AddWindowToCollections(presenter, window, scope);
        window.Show();
        _activeWindow = window;
    }

    public async Task ShowDialogWindowAsync<TPresenter>() where TPresenter : IPresenter
    {
        var scope = _serviceProvider.CreateScope();
        var presenter = scope.ServiceProvider.GetRequiredService<TPresenter>();

        var view = presenter.GetView() as UserControl;
        if (view == null)
        {
            scope.Dispose();
            throw new InvalidOperationException(
                "The view associated with the presenter is not a UserControl or is null.");
        }

        var window = CreateWindow(view, ((IView)view).GetTitle());

        AddWindowToCollections(presenter, window, scope);

        if (_activeWindow == null)
        {
            throw new InvalidOperationException("Application lifetime or active window not set.");
        }

        await window.ShowDialog(_activeWindow);
    }

    public void CloseWindow<TPresenter>() where TPresenter : IPresenter
    {
        var presenter = GetPresenterFromAnyScope<TPresenter>();
        if (presenter != null && _presenterToWindowMap.TryGetValue(presenter, out Window window))
        {
            window.Close();
        }
    }

    public void HideWindow<TPresenter>() where TPresenter : IPresenter
    {
        var presenter = GetPresenterFromAnyScope<TPresenter>();
        if (presenter != null && _presenterToWindowMap.TryGetValue(presenter, out Window window))
        {
            window.Hide();
        }
    }

    private TPresenter GetPresenterFromAnyScope<TPresenter>() where TPresenter : IPresenter
    {
        foreach (var scope in _presenterToScopeMap.Values)
        {
            var presenter =
                scope.ServiceProvider.GetService<TPresenter>();
            if (presenter != null)
            {
                return presenter;
            }
        }

        return default;
    }

    public void Dispose()
    {
        while (_windowStack.TryPop(out var window))
        {
            window.Close();
        }

        foreach (var scope in _presenterToScopeMap.Values)
        {
            scope.Dispose();
        }

        _presenterToScopeMap.Clear();
        _presenterToWindowMap.Clear();
    }

    private void AddWindowToCollections(IPresenter presenter, Window window, IServiceScope scope)
    {
        _presenterToWindowMap[presenter] = window;
        _presenterToScopeMap[presenter] = scope;
        _windowStack.Push(window);

        window.Closed += (s, e) =>
        {
            _windowStack.Pop();
            _presenterToWindowMap.Remove(presenter);
            if (_presenterToScopeMap.TryGetValue(presenter, out var scopeToDispose))
            {
                scopeToDispose.Dispose();
                _presenterToScopeMap.Remove(presenter);
            }

            if (presenter is IDisposable disposablePresenter)
            {
                disposablePresenter.Dispose();
            }
        };
    }

    private void HandleExistingWindow(Window window)
    {
        if (window.IsVisible)
        {
            window.Activate();
        }
        else
        {
            window.Show();
        }
    }

    private Window CreateWindow(UserControl control, string title)
    {
        var window = new Window
        {
            Title = title,
            MinWidth = 800,
            MinHeight = 600,
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            BorderThickness = new Thickness(5),
            BorderBrush = new SolidColorBrush(Colors.Black),
            Width = 1024,
            Height = 768,
            WindowState = WindowState.Maximized,
            Content = new Grid
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Children = { control }
            },
            SystemDecorations = SystemDecorations.Full,
            ShowInTaskbar = true
        };
        return window;
    }
}