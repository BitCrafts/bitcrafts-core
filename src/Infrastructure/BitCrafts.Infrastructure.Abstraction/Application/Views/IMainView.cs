namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IMainView : IWindow
{
    void InitializeMenu(IReadOnlyDictionary<string, IView> views);
}