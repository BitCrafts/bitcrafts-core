using BitCrafts.Infrastructure.Abstraction.Application.Views;

namespace BitCrafts.Infrastructure.Abstraction.Application.Presenters;

public interface IPresenter : IDisposable
{
    /*Task ShowAsync(bool isMainWindow = false);
    Task CloseAsync();
    Task CloseAndOpenPresenterAsync<T>(bool isMainWindow = false);
    Task OpenPresenterAsync<T>(bool isMainWindow = false);*/
    T GetView<T>() where T : IView; 
}