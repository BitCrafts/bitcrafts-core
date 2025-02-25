using System;
using System.Threading.Tasks;
using Avalonia.Controls;
using BitCrafts.Infrastructure.Abstraction.Application;

namespace BitCrafts.Infrastructure.Application.Avalonia;
/*
public sealed class AvaloniaSplashScreen : ISplashScreen
{
    private readonly Window _splashWindow;

    public AvaloniaSplashScreen()
    {
        _splashWindow = new SplashScreen
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen,
            Topmost = true
        };
    }

    public void SetText(string text)
    {
        if (text == null) throw new ArgumentNullException(nameof(text));

        var loadingTextBlock = _splashWindow.
        if (loadingTextBlock == null)
            throw new InvalidOperationException("LoadingTextBlock control not found in the SplashScreen.");

        loadingTextBlock.Text = text;
    }

    public Task ShowAsync()
    {
        _splashWindow.Show();
        return Task.CompletedTask;
    }

    public void Close()
    {
        _splashWindow.Close();
    }

    public T GetNativeObject<T>() where T : class
    {
        return _splashWindow as T ??
               throw new InvalidOperationException($"The splash window cannot be cast to type {typeof(T)}.");
    }

    public void Dispose()
    {
        Close();
    }
}*/