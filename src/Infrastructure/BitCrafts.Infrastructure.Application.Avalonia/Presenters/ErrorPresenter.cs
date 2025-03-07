using System;
using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Application.Avalonia.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public sealed class ErrorPresenter : BasePresenter<IErrorView>, IErrorPresenter
{
    public ErrorPresenter(IServiceProvider serviceProvider) : base("Error", serviceProvider)
    {
    }

    protected override void OnInitialize()
    {
        View.CloseEvent += ViewOnCloseEvent;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.CloseEvent -= ViewOnCloseEvent;
        }

        base.Dispose(disposing);
    }

    private void ViewOnCloseEvent(object sender, EventArgs e)
    {
        ServiceProvider.GetRequiredService<IWindowManager>().CloseWindow(this);
    }

    public void SetException(Exception exception)
    {
        View.SetException(exception);
    }
}