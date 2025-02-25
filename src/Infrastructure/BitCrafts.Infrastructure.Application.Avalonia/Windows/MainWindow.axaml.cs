using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.UI;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public partial class MainWindow : Window, IMainWindow
{
    private readonly ContentControl _content;
    private readonly Dictionary<string, UserControl> _loadedModules;
    private readonly ListBox _menuList;

    public MainWindow()
    {
        AvaloniaXamlLoader.Load(this);

        _menuList = this.FindControl<ListBox>("ModulesListBox") ??
                    throw new InvalidOperationException("ModulesListBox not found in the window.");
        _content = this.FindControl<ContentControl>("ModuleContent") ??
                   throw new InvalidOperationException("ModuleContent not found in the window.");
        _loadedModules = new Dictionary<string, UserControl>();
    }
 
    private void OnModuleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (_menuList.SelectedItem is not TextBlock selectedTextBlock || selectedTextBlock.Text == null)
            return;

        if (_loadedModules.TryGetValue(selectedTextBlock.Text, out var selectedModule))
            _content.Content = selectedModule;
    }

    public void InitializeMenu(IReadOnlyDictionary<string, IView> views)
    {
        if (views == null) throw new ArgumentNullException(nameof(views));
        _menuList.SelectionChanged -= OnModuleSelected;
        _menuList.Items.Clear();
        _loadedModules.Clear();

        foreach (var (name, moduleView) in views)
        {
            if (_loadedModules.TryAdd(name, (UserControl)moduleView))
            {
                _menuList.Items.Add(new TextBlock { Text = name });
            }
        }
        _menuList.SelectionChanged += OnModuleSelected;
    }
}