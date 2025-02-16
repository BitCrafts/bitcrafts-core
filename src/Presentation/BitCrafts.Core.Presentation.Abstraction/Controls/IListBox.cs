namespace BitCrafts.Core.Presentation.Abstraction.Controls;

public interface IListBox
{
    List<string> Items { get; set; }
    string SelectedItem { get; set; }
    event Action<string> OnSelectionChanged;

}