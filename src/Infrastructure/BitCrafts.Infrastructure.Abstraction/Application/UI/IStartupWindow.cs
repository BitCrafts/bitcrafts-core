namespace BitCrafts.Infrastructure.Abstraction.Application.UI;

public interface IStartupWindow : IWindow
{
    void SetLoadingText(string text); 
}