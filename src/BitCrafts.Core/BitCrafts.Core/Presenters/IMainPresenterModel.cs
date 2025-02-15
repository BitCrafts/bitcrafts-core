using Avalonia.Controls;
using BitCrafts.Core.Contracts.Presenters;

namespace BitCrafts.Core.Presenters;

public interface IMainPresenterModel : IPresenterModel
{
    List<(string moduleName, Control widget)> Widgets { get; }
}