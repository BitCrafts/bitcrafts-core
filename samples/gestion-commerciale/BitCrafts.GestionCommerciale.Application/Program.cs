using BitCrafts.Infrastructure.Abstraction.Application;
using BitCrafts.Infrastructure.Application.Avalonia.Extensions;
using BitCrafts.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.GestionCommerciale.Application;

internal class Program
{
    /*[STAThread]
   public static void Main(string[] args) => BuildAvaloniaApp()
       .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
         {
             return AppBuilder.Configure<App>()
                 .UsePlatformDetect()
                 .LogToTrace();
         }*/


    [STAThread]
    private static async Task Main(string[] args)
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection
            .AddBitCraftsInfrastructure()
            .AddBitCraftsAvaloniaApplication();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        var factory = serviceProvider.GetRequiredService<IApplicationFactory>();
        using var app = factory.CreateApplication();
        await app.StartAsync();
    }
}