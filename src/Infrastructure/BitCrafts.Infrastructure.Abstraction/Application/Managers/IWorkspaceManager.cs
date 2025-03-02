using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWorkspaceManager : IDisposable
{
    Task ShowPresenterAsync(IPresenter presenter);
    Task ClosePresenterAsync(IPresenter presenter);
}