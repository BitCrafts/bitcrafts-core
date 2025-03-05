namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IView : IDisposable
{
    event EventHandler ViewLoadedEvent;
    event EventHandler ViewClosedEvent; 

    string GetTitle();
    
    void SetTitle(string title);
}