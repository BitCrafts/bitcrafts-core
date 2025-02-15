using Avalonia.Controls;

namespace BitCrafts.Core.Presenters;

public class MainPresenterModel : IMainPresenterModel
{
    public MainPresenterModel()
    {
        Widgets = new List<(string moduleName, Control widget)>();
    }

    public List<(string moduleName, Control widget)> Widgets { get; }
}