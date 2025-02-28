using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public partial class MainView : Window, IMainView
{
    private TabControl _tabControl;
    private Dictionary<string, IModule> _loadedModules;
    private ListBox _menuList;
    public event EventHandler WindowLoaded;
    public event EventHandler WindowClosed;
    public event EventHandler<string> MenuItemClicked;
    public IWindow Owner { get; set; }

    public MainView()
    {
        AvaloniaXamlLoader.Load(this);
        Loaded += OnLoaded;
        Closed += OnClosed;
    }

    private void OnClosed(object sender, EventArgs e)
    {
        WindowClosed?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        WindowLoaded?.Invoke(this, EventArgs.Empty);
    }


    public void InitializeMenu(IEnumerable<IModule> modules)
    {
        if (modules == null) throw new ArgumentNullException(nameof(modules));
        _menuList.Items.Clear();
        _loadedModules.Clear();
        foreach (var module in modules)
        {
            if (_loadedModules.TryAdd(module.Name, module))
            {
                _menuList.Items.Add(new TextBlock { Text = module.Name });
            }
        }
    }

    private void CreateTabItem()
    {
        _tabControl.Items.Add(new TabItem { Header = "Main", Content = new TextBlock() { Text = "Main" } });
    }

    public void Initialize()
    {
        _menuList = this.FindControl<ListBox>("ModulesListBox");
        _tabControl = this.FindControl<TabControl>("MainTabControl");
        _loadedModules = new Dictionary<string, IModule>();
        _menuList.SelectionChanged += OnModuleSelected;
    }

    public TabControl GetTabControl()
    {
        return _tabControl;
    }

    private void OnModuleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (_menuList.SelectedItem is not TextBlock selectedTextBlock || selectedTextBlock.Text == null)
            return;
        MenuItemClicked?.Invoke(this, selectedTextBlock.Text);
    }

    public void Dispose()
    {
        Loaded -= OnLoaded;
        Closed -= OnClosed;
        _menuList.SelectionChanged -= OnModuleSelected;
        _menuList.Items.Clear();
        _loadedModules.Clear();
    }
}