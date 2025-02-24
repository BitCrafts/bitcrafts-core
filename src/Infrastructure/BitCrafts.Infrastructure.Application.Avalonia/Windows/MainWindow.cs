using System;
using System.Collections.Generic;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application.UI;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public class MainWindow : IWindow
{
    private readonly Window _window;
    private readonly ListBox _menuList;
    private readonly ContentControl _currentContentControl;
    private readonly Dictionary<string, UserControl> _loadedModules;

    public MainWindow()
    {
        _window = new MainNativeWindow();
        _menuList = _window.FindControl<ListBox>("ModulesListBox") ??
                    throw new InvalidOperationException("ModulesListBox not found in the window.");
        _currentContentControl = _window.FindControl<ContentControl>("ModuleContent") ??
                                 throw new InvalidOperationException("ModuleContent not found in the window.");
        _loadedModules = new Dictionary<string, UserControl>();
    }

    public void InitializeMenuList(IReadOnlyDictionary<string, UserControl> modules)
    {
        if (modules == null) throw new ArgumentNullException(nameof(modules));

        _menuList.Items.Clear();
        _loadedModules.Clear();

        foreach (var (name, moduleView) in modules)
        {
            _menuList.Items.Add(new TextBlock { Text = name });
            _loadedModules[name] = moduleView;
        }

        _menuList.SelectionChanged += OnModuleSelected;
    }

    private void OnModuleSelected(object sender, SelectionChangedEventArgs e)
    {
        if (_menuList.SelectedItem is not TextBlock selectedTextBlock || selectedTextBlock.Text == null)
            return;

        if (_loadedModules.TryGetValue(selectedTextBlock.Text, out var selectedModule))
        {
            _currentContentControl.Content = selectedModule;
        }
    }


    public void Show() => _window.Show();


    public void Hide() => _window.Hide();


    public void Close() => _window.Close();

    public T GetNativeWindow<T>() where T : class =>
        _window as T ?? throw new InvalidOperationException($"Window is not of type {typeof(T)}.");
}