using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWindowingManager : IWindowingManager
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<Type, IView> _windows = new();
    private IClassicDesktopStyleApplicationLifetime _applicationLifetime;

    public AvaloniaWindowingManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

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

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        _applicationLifetime = applicationLifetime;
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
    }
}