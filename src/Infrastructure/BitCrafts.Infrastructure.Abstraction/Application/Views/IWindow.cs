namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IWindow : IView
{
    event EventHandler WindowLoaded;
    event EventHandler WindowClosed;
    IWindow ParentWindow { get; set; }
    void Close();
}