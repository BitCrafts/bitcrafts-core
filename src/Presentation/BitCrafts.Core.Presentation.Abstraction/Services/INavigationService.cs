using BitCrafts.Core.Presentation.Abstraction.Views;

namespace BitCrafts.Core.Presentation.Abstraction.Services;

public interface INavigationService
{
    void NavigateTo(IView view);
    void GoBack();

}