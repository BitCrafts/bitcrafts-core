namespace BitCrafts.Infrastructure.Abstraction.Application.UI;

public interface IView
{
    void Show();
    void Hide();
    T GetNativeView<T>() where T : class;
}