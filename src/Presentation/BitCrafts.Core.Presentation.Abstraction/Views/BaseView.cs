using BitCrafts.Core.Contracts.Services;
using BitCrafts.Core.Presentation.Abstraction.Services;

namespace BitCrafts.Core.Presentation.Abstraction.Views;

public abstract class BaseView : IView
{
    protected readonly INavigationService _navigationService;
    protected readonly IEventAggregatorService _eventAggregator;

    public BaseView(INavigationService navigationService, IEventAggregatorService eventAggregator)
    {
        _navigationService = navigationService;
        _eventAggregator = eventAggregator;
    }

    public abstract void Render();
    public abstract void Show();
    public abstract void Close();
    public abstract T GetUserControl<T>();
}