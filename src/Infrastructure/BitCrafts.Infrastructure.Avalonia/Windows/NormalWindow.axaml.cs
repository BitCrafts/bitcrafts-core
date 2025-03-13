using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace BitCrafts.Infrastructure.Avalonia.Windows;

public partial class NormalWindow : Window
{
    public NormalWindow()
    {
        InitializeComponent();
    }

    public void SetContent(UserControl control)
    {
        WindowContent.Content = control;
    }

    public void SetTitle(string title)
    {
        Title = title;
        WindowTitle.Text = title;
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}