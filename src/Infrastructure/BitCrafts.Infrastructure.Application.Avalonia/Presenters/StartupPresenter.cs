using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;
using BitCrafts.Infrastructure.Application.Avalonia.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class StartupPresenter : BasePresenter<IStartupView>, IStartupPresenter
{
    public StartupPresenter(IServiceProvider serviceProvider) : base("Startup", serviceProvider)
    {
    }

    protected override async void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        var view = GetView<StartupView>();
        view.SetLoadingText("Loading Terminated");
        await CloseAndOpenPresenterAsync<IMainPresenter>(true);
    }
}