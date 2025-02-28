using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class StartupPresenter : BasePresenter<IStartupView>, IStartupPresenter
{
    public StartupPresenter(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }

    protected override void OnWindowClosed(object sender, EventArgs e)
    {
    }

    protected override async void OnWindowLoaded(object sender, EventArgs e)
    {
        View.SetLoadingText("Loading Terminated");
        await Task.Delay(1000);
        if (View is IWindow win)
        {
            win.Close();
        }

        var presenter = ServiceProvider.GetRequiredService<IMainPresenter>();
        await presenter.InitializeAsync();
        presenter.Show();
    }

    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
    }

    private string GetModulesPath()
    {
        var modulesPath = Path.Combine(Directory.GetCurrentDirectory(), "Modules");
        if (string.IsNullOrEmpty(modulesPath)) return null;

        return Path.IsPathRooted(modulesPath) ? modulesPath : Path.GetFullPath(modulesPath);
    }

    private async Task RegisterModules(IServiceCollection services)
    {
        var modulesPath = GetModulesPath();
        if (string.IsNullOrEmpty(modulesPath)) return;
        var allFiles = Directory.GetFiles(modulesPath, "*.dll");

        foreach (var dll in allFiles.Where(x => x.Contains("Abstraction")))
        {
            var dllName = Path.GetFileName(dll);
            View.SetLoadingText($"Loading assembly {dllName}");
            await Task.Delay(500);
            AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);
        }

        foreach (var dll in allFiles.Where(x => !x.Contains("Abstraction")))
        {
            var dllName = Path.GetFileName(dll);
            View.SetLoadingText($"Loading assembly {dllName}");
            await Task.Delay(500);
            await LoadAssemblyAsync(dll, services);
        }
    }

    private Task LoadAssemblyAsync(string dll, IServiceCollection services)
    {
        var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(dll);

        return RegisterModulesAsync(assembly, services);
    }

    private async Task RegisterModulesAsync(Assembly assembly, IServiceCollection services)
    {
        try
        {
            var moduleTypes = assembly.GetTypes().Where(IsValidModule);
            foreach (var type in moduleTypes)
            {
                var moduleInstance = Activator.CreateInstance(type) as IModule;
                View.SetLoadingText($"Registering Module {moduleInstance.Name}");
                services.TryAddKeyedSingleton<IModule>(moduleInstance.Name, moduleInstance);
                View.SetLoadingText($"Registering Module Services");
                moduleInstance.RegisterServices(services);
                await Task.Delay(500);
            }
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, $"Error loading assembly {assembly.FullName}");
        }
    }

    private bool IsValidClass(Type type)
    {
        return type.IsClass && !type.IsAbstract;
    }

    private bool IsValidModule(Type type)
    {
        return IsValidClass(type) && typeof(IModule).IsAssignableFrom(type);
    }
}