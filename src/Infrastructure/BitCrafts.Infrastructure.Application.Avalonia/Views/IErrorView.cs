using System;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Application.Avalonia.Views;

public interface IErrorView : IView
{
    public event EventHandler CloseEvent;
    void SetException(Exception exception);
}