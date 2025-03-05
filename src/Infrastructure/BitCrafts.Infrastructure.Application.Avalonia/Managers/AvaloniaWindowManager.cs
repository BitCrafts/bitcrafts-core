using System;
using System.Collections.Generic;
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
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private readonly Stack<Window> _windowStack = new();
    private readonly Dictionary<Type, Window> _presenterToWindowMap = new();

    public AvaloniaWindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        _applicationLifetime = applicationLifetime;
    }

    public void ShowWindow<TPresenter>() where TPresenter : IPresenter
    {
        if (_presenterToWindowMap.ContainsKey(typeof(TPresenter)))
        {
            Window existingWindow = _presenterToWindowMap[typeof(TPresenter)];
            if (existingWindow.IsVisible)
            {
                existingWindow.Activate();
            }
            else
            {
                existingWindow.Show();
            }

            return;
        }

        var presenter = _serviceProvider.GetRequiredService<TPresenter>();
        var view = presenter.GetView<IView>();
        Window window = CreateWindow(view as UserControl, view.GetTitle());
        _presenterToWindowMap.TryAdd(typeof(TPresenter), window);
        _windowStack.Push(window);

        window.Closed += (s, e) =>
        {
            _windowStack.Pop();
            _presenterToWindowMap.Remove(typeof(TPresenter));
        };
        window.Show();
    }

    public void CloseWindow<TPresenter>() where TPresenter : IPresenter
    {
        if (_presenterToWindowMap.TryGetValue(typeof(TPresenter), out Window window))
        {
            window.Close();
        }
    }

    public void HideWindow<TPresenter>() where TPresenter : IPresenter
    {
        if (_presenterToWindowMap.TryGetValue(typeof(TPresenter), out Window window))
        {
            window.Hide();
        }
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    private Window CreateWindow(UserControl control, string title)
    {
        var window = new Window();
        window.Title = title;
        window.MinWidth = 800;
        window.MinHeight = 600;
        window.Width = 1024;
        window.Height = 768;
        window.WindowState = WindowState.Maximized;
        window.Content = new Grid()
        {
            VerticalAlignment = VerticalAlignment.Stretch,
            HorizontalAlignment = HorizontalAlignment.Stretch,
            Children = { control }
        };
        window.SystemDecorations = SystemDecorations.Full;
        window.ShowInTaskbar = true;
        window.WindowStartupLocation = WindowStartupLocation.CenterScreen; 
        window.BorderThickness = new Thickness(5, 5,5,5);
        window.BorderBrush = new SolidColorBrush(Colors.Black);
        
        return window;
    }
/*
    public void ShowWindow(IView window, bool setAsMainWindow = false)
    {
        var nativeWindow = (Window)window;
        if (nativeWindow == null) return;

        if (!_windows.ContainsKey(window.GetType()))
        {
            if (setAsMainWindow)
            {
                _applicationLifetime.MainWindow = nativeWindow;
            }

            _windows.TryAdd(window.GetType(), window);
        }

        nativeWindow.Show();
    }


    public void HideWindow(IView window)
    {
        var nativeWindow = (Window)window;
        if (nativeWindow == null) return;

        nativeWindow.Hide();
    }

    public void CloseWindow(IView window)
    {
        var nativeWindow = (Window)window;
        if (nativeWindow == null) return;
        if (_windows.ContainsKey(window.GetType()))
        {
            _windows.Remove(window.GetType());
            nativeWindow.Close();
        }
    }


    public T GetWindow<T>() where T : IView
    {
        _windows.TryGetValue(typeof(T), out var window);
        return (T)window;
    }

    public IReadOnlyCollection<IView> GetAllWindows()
    {
        return _windows.Values;
    }



    public void Dispose()
    {
        foreach (var window in _windows.Values)
        {
            if (window is IDisposable disposableWindow)
            {
                disposableWindow.Dispose();
            }
        }

        _windows.Clear();
    }*/
}