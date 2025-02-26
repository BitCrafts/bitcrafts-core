using System;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class MainPresenter : BasePresenter<IMainView>,IMainPresenter
{
    public MainPresenter(IServiceProvider serviceProvider) : base(serviceProvider)
    {
    }
}