namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IView : IDisposable
{
    event EventHandler ViewLoadedEvent;
    event EventHandler ViewClosedEvent;
    bool IsWindow { get; }
    IView ParentView { get; set; }
    void SetTitle(string title);
    string GetTitle(string title);
}