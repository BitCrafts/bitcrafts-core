using System;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Windows;

public partial class StartupView : Window, IStartupView
{
    private readonly IServiceProvider _serviceProvider;
    public event EventHandler ViewLoadedEvent;
    public event EventHandler ViewClosedEvent;
    public bool IsWindow => true;
    public IView ParentView { get; set; }


    public StartupView(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        InitializeComponent();

        Loaded += OnLoaded;
        Closed += OnClosed;
    }


    private void OnClosed(object sender, EventArgs e)
    {
        ViewClosedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewLoadedEvent?.Invoke(this, EventArgs.Empty);
    }

    public void SetLoadingText(string text)
    {
        if (LoadingTextBlock != null)
        {
            LoadingTextBlock.Text = text;
        }
    }


    public void SetTitle(string title)
    {
        Title = title;
    }

    public string GetTitle()
    {
        return Title;
    }

    public void Dispose()
    {
        _serviceProvider.GetRequiredService<ILogger<StartupView>>().LogInformation($"Disposing {this.GetType()}");
        Loaded -= OnLoaded;
        Closed -= OnClosed;
    }
}