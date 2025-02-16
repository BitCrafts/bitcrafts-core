namespace BitCrafts.Core.Presentation.Abstraction.Controls;

public interface ITextBox
{
    string Text { get; set; }
    event Action<string> OnTextChanged;

}