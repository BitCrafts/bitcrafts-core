namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IStartupView : IView
{
    void SetLoadingText(string text); 
}