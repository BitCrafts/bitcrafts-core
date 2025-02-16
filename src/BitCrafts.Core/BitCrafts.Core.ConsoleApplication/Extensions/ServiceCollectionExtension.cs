using BitCrafts.Core.ConsoleApplication.Presenters;
using BitCrafts.Core.ConsoleApplication.Services;
using BitCrafts.Core.ConsoleApplication.Views;
using BitCrafts.Core.Contracts.Applications;
using BitCrafts.Core.Presentation.Abstraction.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Core.ConsoleApplication.Extensions;

public static class ServiceCollectionExtension
{
    public static void AddConsoleApplication(this IServiceCollection services)
    {
        services.AddTransient<IMainView, ConsoleMainView>();
        services.AddTransient<IMainPresenter, ConsoleMainPresenter>(); 
        services.AddTransient<IMainPresenterModel, ConsoleMainPresenterModel>(); 
        services.AddSingleton<INativeApplication,ConsoleNativeApplication>();
        services.AddSingleton<INavigationService, ConsoleNavigationService>();
    }
}