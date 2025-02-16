using BitCrafts.Core.Presentation.Abstraction.Controls;

namespace BitCrafts.Core.ConsoleApplication.Controls;

public class ConsoleButton : IButton
{
    public string Text { get; set; }
    public event Action OnClick;

    public void Click()
    {
        OnClick?.Invoke();
    }
}