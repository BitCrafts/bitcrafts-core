using System.Collections.Generic;
using Avalonia.Controls;

namespace BitCrafts.Infrastructure.Application.Avalonia;

public partial class MainWindow : Window
{
    private Dictionary<string, UserControl> _modules = new();

    public MainWindow()
    {
        InitializeComponent();

        // Ajoutez vos modules (remplacez par vos modules réels)
        //_modules.Add("Module 1", new Module1View());
        //_modules.Add("Module 2", new Module2View());


        // Remplissez la ListBox avec les noms des modules
        foreach (var moduleName in _modules.Keys)
        {
            ModulesListBox.Items.Add(moduleName);
        }
    }

    private void ModulesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ModulesListBox.SelectedItem is string selectedModuleName)
        {
            if (_modules.TryGetValue(selectedModuleName, out var selectedModuleView))
            {
                ModuleContent.Content = selectedModuleView;
            }
        }
    }
}