using System;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public interface IErrorPresenter : IPresenter
{
    void SetException(Exception exception);
}