using BitCrafts.Core.Contracts.Applications.Presenters;
using BitCrafts.Core.Views;
using Gtk;

namespace BitCrafts.Core.Presenters;

public interface IMainPresenter : IPresenter<IMainWindowView>
{
    public List<(string moduleName, Widget widget)> GetResolvedWidgets();
}