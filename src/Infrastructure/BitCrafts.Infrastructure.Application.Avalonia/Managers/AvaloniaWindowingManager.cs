using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

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

    public void ShowWindow(IWindow window)
    {
        var nativeWindow = (Window)window;
        if (nativeWindow == null) return;
        if (!_windows.ContainsKey(window.GetType()))
        {
            _windows.TryAdd(window.GetType(), window);
        }

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


    public void HideWindow(IWindow window)
    {
        if (window is Window wnd)
        {
            wnd.Hide();
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
        _applicationLifetime.ShutdownMode = ShutdownMode.OnMainWindowClose;
    }
}