using System;

namespace BitCrafts.Infrastructure.Application.Avalonia.Presenters;

public interface IErrorPresenter
{
    void SetException(Exception exception);
}