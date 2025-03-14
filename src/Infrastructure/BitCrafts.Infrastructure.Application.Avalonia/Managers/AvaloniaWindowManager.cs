﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Avalonia.Dialogs;
using BitCrafts.Infrastructure.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Managers;

public sealed class AvaloniaWindowManager : IWindowManager
{
    private readonly Dictionary<IPresenter, Window> _presenterToWindowMap = new();
    private readonly IServiceProvider _serviceProvider;
    private readonly Stack<Window> _windowStack = new();
    private Window _activeWindow;

    public AvaloniaWindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void ShowWindow<TPresenter>() where TPresenter : class, IPresenter
    {
        var presenterType = typeof(TPresenter);
        if (HasExistingPresenter(presenterType))
            return;
        var presenter = _serviceProvider.GetRequiredService(presenterType) as IPresenter;
        if (presenter != null && _presenterToWindowMap.ContainsKey(presenter))
        {
            HandleExistingWindow(_presenterToWindowMap[presenter]);
            return;
        }

        if (presenter != null)
        {
            var view = presenter.GetView() as UserControl;
            if (view == null)
                throw new InvalidOperationException("The view associated with the presenter is not a UserControl.");

            var window = CreateWindow(view, ((IView)view).GetTitle());
            AddWindowToCollections(presenter, window);
            window.Show();
            _activeWindow = window;
        }
    }

    public async Task ShowDialogWindowAsync<TPresenter>() where TPresenter : class, IPresenter
    {
        var presenterType = typeof(TPresenter);
        if (HasExistingPresenter(presenterType))
            return;
        var presenter = _serviceProvider.GetRequiredService(presenterType) as IPresenter;

        if (presenter != null)
        {
            var view = presenter.GetView() as UserControl;
            if (view == null)
                throw new InvalidOperationException(
                    "The view associated with the presenter is not a UserControl or is null.");

            var window = CreateDialog(view, ((IView)view).GetTitle());

            AddWindowToCollections(presenter, window);

            if (_activeWindow == null)
                throw new InvalidOperationException("Application lifetime or active window not set.");

            await window.ShowDialog(_activeWindow);
        }
    }

    public void CloseWindow<TPresenter>() where TPresenter : class, IPresenter
    {
        var presenter = GetPresenterFromAnyScope<TPresenter>();
        if (presenter != null && _presenterToWindowMap.TryGetValue(presenter, out var window)) window.Close();
    }

    public void HideWindow<TPresenter>() where TPresenter : class, IPresenter
    {
        var presenter = GetPresenterFromAnyScope<TPresenter>();
        if (presenter != null && _presenterToWindowMap.TryGetValue(presenter, out var window)) window.Hide();
    }

    public void Dispose()
    {
        while (_windowStack.TryPop(out var window)) window.Close();

        _presenterToWindowMap.Clear();
    }

    private TPresenter GetPresenterFromAnyScope<TPresenter>() where TPresenter : IPresenter
    {
        foreach (var presenter in _presenterToWindowMap.Keys)
            if (presenter is TPresenter presenterToWindow)
                return presenterToWindow;

        return default;
    }

    private bool HasExistingPresenter(Type presenterType)
    {
        foreach (var presenter in _presenterToWindowMap.Keys)
            if (presenter.GetType() == presenterType)
                return true;

        return false;
    }

    private void AddWindowToCollections(IPresenter presenter, Window window)
    {
        _presenterToWindowMap[presenter] = window;
        _windowStack.Push(window);

        window.Closed += (_, _) =>
        {
            _windowStack.Pop();
            _presenterToWindowMap.Remove(presenter);

            if (presenter is IDisposable disposablePresenter) disposablePresenter.Dispose();
        };
    }

    private void HandleExistingWindow(Window window)
    {
        if (window.IsVisible)
            window.Activate();
        else
            window.Show();
    }

    private Window CreateDialog(UserControl control, string title)
    {
        var window = new DialogWindow();
        window.Title = title;
        window.SetContent(control);
        window.SetSize(control.Width, control.Height);
        return window;
    }

    private Window CreateWindow(UserControl control, string title)
    {
        var window = new NormalWindow();
        window.SetTitle(title);
        window.SetContent(control);
        return window;
    }
}