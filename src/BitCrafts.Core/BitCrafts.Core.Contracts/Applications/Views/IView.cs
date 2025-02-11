namespace BitCrafts.Core.Contracts.Applications.Views;

public interface IView
{
    void InitializeView();
    void ShowView();
    void CloseView();
}