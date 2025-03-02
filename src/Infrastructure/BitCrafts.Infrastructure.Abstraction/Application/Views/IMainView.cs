using BitCrafts.Infrastructure.Abstraction.Modules;

namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IMainView : IView
{
    event EventHandler<string> MenuItemClicked;
    void InitializeMenu(IEnumerable<IModule> modules);
}