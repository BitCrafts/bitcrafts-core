using System;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views; 

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class StartupPresenter : BasePresenter<IStartupView>, IPresenter<IStartupView>
{
    public StartupPresenter(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}