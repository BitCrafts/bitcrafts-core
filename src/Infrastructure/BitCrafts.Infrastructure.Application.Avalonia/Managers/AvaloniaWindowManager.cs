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
    private Window _activeWindow;
    private readonly Dictionary<Type, Window> _presenterToWindowMap = new();

    public AvaloniaWindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetNativeApplication(IClassicDesktopStyleApplicationLifetime applicationLifetime)
    {
        _applicationLifetime = applicationLifetime;
    }
    public async void ShowWindow(IPresenter presenter, bool isModal = false)
    {
        Type presenterType = presenter.GetType();
        if (_presenterToWindowMap.ContainsKey(presenterType))
        {
            Window existingWindow = _presenterToWindowMap[presenterType];
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

        var view = presenter.GetView();
        Window window = CreateWindow(view as UserControl, view.GetTitle(), isModal);
        _presenterToWindowMap.TryAdd(presenterType, window);
        _windowStack.Push(window);

        window.Closed += (s, e) =>
        {
            _windowStack.Pop();
            _presenterToWindowMap.Remove(presenterType);
        };


        if (!isModal)
        {
            _activeWindow = window;
            window.Show();
        }
        else
        {
            await window.ShowDialog(_activeWindow);
        }
    }

    public void CloseWindow(IPresenter presenter)
    {
        if (_presenterToWindowMap.TryGetValue(presenter.GetType(), out Window window))
        {
            window.Close();
        }
    }

    public void HideWindow(IPresenter presenter)
    {
        if (_presenterToWindowMap.TryGetValue(presenter.GetType(), out Window window))
        {
            window.Hide();
        }
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    private Window CreateWindow(UserControl control, string title, bool isModal)
    {
        var window = new Window();
        window.Title = title;
        window.MinWidth = 800;
        window.MinHeight = 600;

        window.WindowStartupLocation = WindowStartupLocation.CenterScreen;
        window.BorderThickness = new Thickness(5, 5, 5, 5);
        window.BorderBrush = new SolidColorBrush(Colors.Black);
        if (isModal)
        {
            window.MinWidth = window.Width = control.Width;
            window.MinHeight = window.Height = control.Height;
        }
        else
        {
            window.Width = 1024;
            window.Height = 768;
        }

        window.WindowState = isModal ? WindowState.Normal : WindowState.Maximized;
        if (isModal)
        {
            window.Content = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Children =
                {
                    new Border()
                    {
                        BorderBrush = Brushes.Gray,
                        BorderThickness = new Thickness(2),
                        Child = control
                    }
                }
            };
        }
        else
        {
            window.Content = new Grid()
            {
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                Children =
                {
                    control
                }
            };
        }

        if (isModal)
        {
            window.SystemDecorations = SystemDecorations.None;
            window.ShowInTaskbar = true;
        }
        else
        {
            window.SystemDecorations = SystemDecorations.Full;
            window.ShowInTaskbar = true;
        }


        return window;
    }
}