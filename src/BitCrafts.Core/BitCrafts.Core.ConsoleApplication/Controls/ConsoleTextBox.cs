using BitCrafts.Core.Presentation.Abstraction.Controls;

namespace BitCrafts.Core.ConsoleApplication.Controls;

public class ConsoleTextBox : ITextBox
{
    public string Text { get; set; }
    public event Action<string> OnTextChanged;

    public void Input(string input)
    {
        Text = input;
        OnTextChanged?.Invoke(input);
    }
}