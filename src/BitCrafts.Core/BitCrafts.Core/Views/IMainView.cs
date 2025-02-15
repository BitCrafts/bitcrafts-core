using BitCrafts.Core.Contracts.Views;
using BitCrafts.Core.Presenters;

namespace BitCrafts.Core.Views;

public interface IMainView : IView<IMainPresenterModel>
{
    void InitializeModules();
}