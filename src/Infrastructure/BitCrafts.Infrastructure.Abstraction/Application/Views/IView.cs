namespace BitCrafts.Infrastructure.Abstraction.Application.Views;

public interface IView : IDisposable
{
    void Initialize();
    void Show();
    void Hide();
}