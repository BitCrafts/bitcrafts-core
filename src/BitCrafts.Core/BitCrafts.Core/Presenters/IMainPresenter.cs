using BitCrafts.Core.Contracts.Presenters;
using BitCrafts.Core.Views;

namespace BitCrafts.Core.Presenters;

public interface IMainPresenter : IPresenter<IMainView>
{
    void InitializeModules();
}