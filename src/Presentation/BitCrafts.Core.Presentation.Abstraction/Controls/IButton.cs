namespace BitCrafts.Core.Presentation.Abstraction.Controls;

public interface IButton
{
    string Text { get; set; }
    event Action OnClick;

}