using BitCrafts.Infrastructure.Abstraction.Application.Presenters;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWorkspaceManager : IDisposable
{
    void ShowPresenter(IPresenter presenter);
    void ClosePresenter(IPresenter presenter);
}