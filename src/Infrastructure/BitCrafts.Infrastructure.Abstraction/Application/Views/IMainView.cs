using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IMainView : IWindow
{
    event EventHandler<string> MenuItemClicked;
    void InitializeMenu(IEnumerable<IModule> modules);
}