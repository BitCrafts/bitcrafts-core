using BitCrafts.Core.Presentation.Abstraction.Controls;

namespace BitCrafts.Core.ConsoleApplication.Controls;

public class ConsoleListBox : IListBox
{
    public List<string> Items { get; set; } = new List<string>();
    public string SelectedItem { get; set; }
    public event Action<string> OnSelectionChanged;

    public void Select(string item)
    {
        SelectedItem = item;
        OnSelectionChanged?.Invoke(item);
    }

}