using BitCrafts.Core.Contracts.Views;
using Gtk;

namespace BitCrafts.Core.Views;

public interface IMainView : IView
{
    void InitializeModules(List<(string moduleName, Widget widget)> modulesWidgets);
}