using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWorkspaceManager
{
    void ShowPresenter(string title, dynamic presenter);
    void ClosePresenter(string title);
}