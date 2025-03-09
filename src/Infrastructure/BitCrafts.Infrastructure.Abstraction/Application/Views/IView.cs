namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IView : IDisposable
{
    event EventHandler ViewLoadedEvent;
    event EventHandler ViewClosedEvent;
    bool IsModal { get; }
    void SetBusy(string message);
    void UnsetBusy();

    string GetTitle();

    void SetTitle(string title);
}