using System;
using System.Collections.Generic;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Application.Avalonia.Views;

public interface IMainView : IView
{
    event EventHandler<string> MenuItemClicked;
    void InitializeModulesMenu(IEnumerable<IModule> modules);
}