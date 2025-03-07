using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Views;

public partial class ErrorView : BaseView, IErrorView
{
    public ErrorView(IServiceProvider serviceProvider) : base(serviceProvider)
    {
        InitializeComponent();
    }

    public event EventHandler CloseEvent;

    public void SetException(Exception exception)
    {
        MessageTextBox.Text = exception.Message;
        StackTraceMessage.Text = exception.StackTrace;
    }

    private void OkButton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseEvent?.Invoke(this, EventArgs.Empty);
    }
}