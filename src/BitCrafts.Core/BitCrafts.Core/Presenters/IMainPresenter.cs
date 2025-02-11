using BitCrafts.Core.Contracts.Applications.Presenters;
using BitCrafts.Core.Views;

namespace BitCrafts.Core.Presenters;

public interface IMainPresenter : IPresenter<IMainWindowView>
{
    public List<(string moduleName, Gtk.Widget widget)> GetResolvedWidgets();
}