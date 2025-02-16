namespace BitCrafts.Core.Presentation.Abstraction.Views;

public interface IView
{
    void Render();
    void Show();
    void Close();
    
    T GetUserControl<T>();
}