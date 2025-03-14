namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IView : IDisposable
{
    bool IsView { get; }
    bool IsDialog { get; }
    bool IsWindow { get; }
    string Title { get; }
    bool IsBusy { get; }
    string BusyText { get; } 
    void SetBusy(string text);
    void UnsetBusy();
}