namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IView : IDisposable
{
    event EventHandler ViewLoadedEvent;
    event EventHandler ViewClosedEvent;
    bool IsWindow { get; }
    IView ParentView { get; set; }
     
    string GetTitle();
    
    void SetTitle(string title);
}