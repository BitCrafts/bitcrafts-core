namespace BitCrafts.Core.Contracts.Views;

public interface IView : IDisposable
{
    void Show();
    event EventHandler<EventArgs> Loaded;
    event EventHandler<EventArgs> Unloaded;
}