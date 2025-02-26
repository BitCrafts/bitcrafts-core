using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public class AvaloniaWindowingManager : IWindowingManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, IWindow> _windows = new();
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;
    private Window _lastVisibleWindow;

    public AvaloniaWindowingManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public T CreateWindow<T>() where T : IWindow, new()
    {
        var type = typeof(T);
        if (!_windows.ContainsKey(type))
        {
            var window = new T();
            _windows[type] = window;
        }

        return (T)_windows[type];
    }

    public void ShowWindow<T>() where T : IWindow, new()
    {
        var window = CreateWindow<T>() as Window;
        if (window == null) return;

        if (_lastVisibleWindow != null && _lastVisibleWindow.IsVisible)
        {
            ((IWindow)window).Owner = (IWindow)_lastVisibleWindow;
        }

        if (!window.IsVisible)
        {
            window.Show();
            SetRootWindow(window);
        }
        else
        {
            window.Focus();
        }
    }

    public void ShowWindow(IWindow window)
    {
        var nativeWindow = (Window)window;
        if (nativeWindow == null) return;

        if (_lastVisibleWindow != null && _lastVisibleWindow.IsVisible)
        {
            window.Owner = (IWindow)_lastVisibleWindow;
        }

        if (!nativeWindow.IsVisible)
        {
            nativeWindow.Show();
            SetRootWindow(nativeWindow);
        }
        else
        {
            nativeWindow.Focus();
        }
    }

    public void SetRootWindow(IWindow rootWindow)
    {
        _lastVisibleWindow = (Window)rootWindow;
    }


    public void HideWindow<T>() where T : IWindow
    {
        var window = GetWindow<T>() as Window;
        if (window != null && window.IsVisible)
        {
            window.Hide();
        }
    }

    public void CloseWindow<T>() where T : IWindow
    {
        var window = GetWindow<T>() as Window;
        if (window != null)
        {
            if (_lastVisibleWindow == window)
            {
                _lastVisibleWindow = null;
            }

            window.Close();
            _windows.Remove(typeof(T));
        }
    }


    public void CloseWindow(IWindow window)
    {
        if (window != null)
        {
            if (_lastVisibleWindow == window)
            {
                _lastVisibleWindow = null;
            }

            window.Close();
            _windows.Remove(window.GetType());
        }
    }

    public void SetRootWindow(Window rootWindow)
    {
        _lastVisibleWindow = rootWindow;
    }

    public T GetWindow<T>() where T : IWindow
    {
        _windows.TryGetValue(typeof(T), out var window);
        return (T)window;
    }

    public IReadOnlyCollection<IWindow> GetAllWindows()
    {
        return _windows.Values;
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        _applicationLifetime = applicationLifetime;
    }
}