namespace BitCrafts.Infrastructure.Abstraction.Application.UI;

public interface IMainWindow : IWindow
{
    void InitializeMenu(IReadOnlyDictionary<string, IView> views);
}