namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IStartupView : IWindow
{
    void SetLoadingText(string text); 
}