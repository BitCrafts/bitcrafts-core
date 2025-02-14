using BitCrafts.Core.Contracts.Presenters;
using BitCrafts.Core.Views;
using Gtk;

namespace BitCrafts.Core.Presenters;

public interface IMainPresenter : IPresenter<IMainView>
{
    public List<(string moduleName, Widget widget)> GetResolvedWidgets();
}