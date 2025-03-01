using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public partial class StartupView : Window, IStartupView
{
    private TextBlock _loadingTextBlock;

    public StartupView()
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

    public void SetLoadingText(string text)
    {
        if (_loadingTextBlock != null)
        {
            _loadingTextBlock.Text = text;
        }
    }

    public event EventHandler WindowLoaded;
    public event EventHandler WindowClosed;

    public void Initialize()
    {
        _loadingTextBlock = this.FindControl<TextBlock>("LoadingTextBlock");
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    public IWindow ParentWindow { get; set; }
}