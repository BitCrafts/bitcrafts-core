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
    private Dictionary<string, IModule> _loadedModules;

    public event EventHandler<string> MenuItemClicked;
    public event EventHandler ViewLoadedEvent;
    public event EventHandler ViewClosedEvent;
    public bool IsWindow { get; }
    public IView ParentView { get; set; }

    public MainView()
    {
        IsWindow = true;
        _loadedModules = new Dictionary<string, IModule>();
        InitializeComponent();
        Loaded += OnLoaded;
        Closed += OnClosed;
    }

    private void OnClosed(object sender, EventArgs e)
    {
        ViewClosedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewLoadedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SetTitle(string title)
    {
        Title = title;
    }

    public string GetTitle(string title)
    {
        return Title;
    }

    public void InitializeMenu(IEnumerable<IModule> modules)
    {
        if (modules == null) throw new ArgumentNullException(nameof(modules));
        ModulesListBox.Items.Clear();
        _loadedModules.Clear();
        foreach (var module in modules)
        {
            if (_loadedModules.TryAdd(module.Name, module))
            {
                ModulesListBox.Items.Add(new TextBlock { Text = module.Name });
            }
        }
    }

    private void CreateTabItem()
    {
        MainTabControl.Items.Add(new TabItem { Header = "Main", Content = new TextBlock() { Text = "Main" } });
    }


    public TabControl GetTabControl()
    {
        return MainTabControl;
    }

    public void Dispose()
    {
        Loaded -= OnLoaded;
        Closed -= OnClosed;
        ModulesListBox.SelectionChanged -= ModulesListBox_OnSelectionChanged;
        ModulesListBox.Items.Clear();
        _loadedModules.Clear();
    }

    private void ModulesListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ModulesListBox.SelectedItem is not TextBlock selectedTextBlock || selectedTextBlock.Text == null)
            return;
        MenuItemClicked?.Invoke(this, selectedTextBlock.Text);
    }
}