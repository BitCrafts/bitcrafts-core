using System;
using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.UI;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public partial class MainNativeWindow : Window
{
    private IReadOnlyDictionary<string, IModule> _modules;
    private ListBox _menuList;
    private ContentControl _content;

    public MainNativeWindow()
    {
        AvaloniaXamlLoader.Load(this);
        _menuList = this.FindControl<ListBox>("ModulesListBox");
        _content = this.FindControl<ContentControl>("ModuleContent");
    }

    public void InitializeMenuList(IReadOnlyDictionary<string, IModule> modules)
    {
        _modules = modules;
        _menuList.Items.Clear();
        foreach (var module in modules)
        {
            _menuList.Items.Add(new TextBlock() { Text = module.Key });
            _menuList.Items.Add(new Separator());
        }
    }

    private void ModulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (_menuList.SelectedItem is string selectedModuleName)
        {
            if (_modules.TryGetValue(selectedModuleName, out var selectedModuleView))
            {
                var activatorInstance = (IView)Activator.CreateInstance(selectedModuleView.GetViewType());
                _content.Content = activatorInstance?.GetNativeView<UserControl>();
            }
        }
    }
}