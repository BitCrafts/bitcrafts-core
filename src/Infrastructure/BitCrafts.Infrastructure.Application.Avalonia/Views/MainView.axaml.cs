using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia.Views;

public partial class MainView : UserControl, IMainView
{
    public event EventHandler ViewLoadedEvent;
    public event EventHandler ViewClosedEvent;
    public event EventHandler<string> MenuItemClicked;
    private string _title;

    public MainView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, EventArgs e)
    {
        ViewClosedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewLoadedEvent?.Invoke(this, EventArgs.Empty);
    }


    public void SetTitle(string title)
    {
        _title = title;
    }

    public string GetTitle()
    {
        return _title;
    }

    public void Dispose()
    {
        Loaded -= OnLoaded;
        Unloaded -= OnUnloaded;
        foreach (MenuItem menuItem in ModulesMenuItem.Items)
        {
            menuItem.Click -= MenuItemOnClick;
        }

        ModulesMenuItem.Items.Clear();
    }


    public void InitializeModulesMenu(IEnumerable<IModule> modules)
    {
        if (modules == null) throw new ArgumentNullException(nameof(modules));
        foreach (var module in modules)
        {
            var menuItem = new MenuItem() { Header = module.Name };
            menuItem.Click += MenuItemOnClick;
            ModulesMenuItem.Items.Add(menuItem);
        }
    }

    private void MenuItemOnClick(object sender, RoutedEventArgs e)
    {
        e.Handled = true;
        var menuItem = (MenuItem)sender;
        MenuItemClicked?.Invoke(this, menuItem.Header?.ToString());
    }
}