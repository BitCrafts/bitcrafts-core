namespace BitCrafts.Infrastructure.Abstraction.Application.UI;

public interface IWindow
{
    void Show();
    void Hide();
    void Close();
    T GetNativeWindow<T>() where T : class;
}