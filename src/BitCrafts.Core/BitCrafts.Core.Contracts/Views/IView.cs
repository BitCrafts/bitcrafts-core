namespace BitCrafts.Core.Contracts.Views;

public interface IView<T> : IDisposable
{
    T Model { get; set; }
    public event EventHandler OnViewLoaded;
    public event EventHandler OnViewClosing;
    void ShowView();
    void HideView();
}