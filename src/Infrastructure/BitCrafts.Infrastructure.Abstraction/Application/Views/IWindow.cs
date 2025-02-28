namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IWindow : IView
{
    event EventHandler WindowLoaded;
    event EventHandler WindowClosed;
    
    IWindow Owner { get; set; }
    void Close();
}