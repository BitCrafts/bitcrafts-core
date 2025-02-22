using Avalonia;
using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Application.Avalonia;
using BitCrafts.Infrastructure.Application.Console.Extensions;
using BitCrafts.Infrastructure.Application.Avalonia.Extensions;
using BitCrafts.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.GestionCommerciale.Application;

internal class Program
{
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
/*
    [STAThread]
    static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection
            .AddBitCraftsInfrastructure()
            .AddBitCraftsConsoleApplication()
            .AddBitCraftsAvaloniaApplication();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var factory = serviceProvider.GetRequiredService<IApplicationFactory>();
        using var app = factory.CreateApplication();
        await app.StartAsync();
    }*/
    public static AppBuilder BuildAvaloniaApp()
    {
        return AppBuilder.Configure<App>()
            .UsePlatformDetect()
            .LogToTrace();
    }

}