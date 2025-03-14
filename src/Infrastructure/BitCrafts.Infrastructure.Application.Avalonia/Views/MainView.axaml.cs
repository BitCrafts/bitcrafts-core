using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Avalonia.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia.Views;

public partial class MainView : BaseView, IMainView
{
    public event EventHandler<string> MenuItemClicked;

    public MainView()
    {
        InitializeComponent();
    }

    public void InitializeModulesMenu(IEnumerable<IModule> modules)
    {
        if (modules == null) throw new ArgumentNullException(nameof(modules));
        foreach (var module in modules)
        {
            var menuItem = new MenuItem { Header = module.Name };
            menuItem.Click += MenuItemOnClick;
            ModulesMenuItem.Items.Add(menuItem);
        }
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (MenuItem menuItem in ModulesMenuItem.Items) menuItem.Click -= MenuItemOnClick;

            ModulesMenuItem.Items.Clear();
        }

        base.Dispose(disposing);
    }

    private void MenuItemOnClick(object sender, RoutedEventArgs e)
    {
        e.Handled = true;
        var menuItem = (MenuItem)sender;
        MenuItemClicked?.Invoke(this, menuItem.Header?.ToString());
    }

    public override void SetTitle(string title)
    {
        base.SetTitle(title);
        
    }
}