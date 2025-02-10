namespace BitCrafts.Core.Contracts.Applications.Views;

public interface IView
{
    void ShowView();
    void CloseView();
    event EventHandler OnViewLoaded;
    event EventHandler OnViewClosed;
}