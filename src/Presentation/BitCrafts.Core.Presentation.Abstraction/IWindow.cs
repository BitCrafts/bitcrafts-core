using BitCrafts.Core.Presentation.Abstraction.Views;

namespace BitCrafts.Core.Presentation.Abstraction;

public interface IWindow
{
    void Show();
    void Close();
    void SetContent(IView content);

}