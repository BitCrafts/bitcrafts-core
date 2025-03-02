using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Managers;

public interface IWorkspaceManager
{
    Task ShowPresenterAsync(dynamic presenter);
    Task ClosePresenterAsync(Type presenterType);
}