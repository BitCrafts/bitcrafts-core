using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.UI;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public partial class StartupWindow : Window, IStartupWindow
{
    public StartupWindow()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void SetLoadingText(string text)
    {
        var textBlock = this.FindControl<TextBlock>("LoadingTextBlock");
        if (textBlock != null)
        {
            textBlock.Text = text;
        }
    }
}