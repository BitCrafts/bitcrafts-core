using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public partial class StartupView : Window, IStartupView
{
    private TextBlock _loadingTextBlock;

    public StartupView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void SetLoadingText(string text)
    {
        if (_loadingTextBlock != null)
        {
            _loadingTextBlock.Text = text;
        }
    }

    public void Initialize()
    {
        _loadingTextBlock = this.FindControl<TextBlock>("LoadingTextBlock");
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }
}