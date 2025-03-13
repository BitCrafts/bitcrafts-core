using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace BitCrafts.Infrastructure.Avalonia.Dialogs;

public partial class DialogWindow : Window
{
    public DialogWindow()
    {
        InitializeComponent();
    }

    public void SetContent(UserControl control)
    {
        DialogContent.Content = control;
    }

    public void SetTitle(string title)
    {
        Title = title;
        WindowTitle.Text = title;
    }

    public void SetSize(double width, double height)
    {
        Width = width;
        Height = height;
    }

    private void CloseButton_OnClick(object sender, RoutedEventArgs e)
    {
        Close();
    }
}