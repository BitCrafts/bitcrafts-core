using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Core.Presenters;

namespace BitCrafts.Core.Views;

public class MainWindowView : Gtk.Window, IMainWindowView
{
    private readonly IMainPresenter _presenter;

    public MainWindowView(IMainPresenter presenter)
    {
        _presenter = presenter;
        _presenter.SetView(this);
        DefaultWidth = 800;
        DefaultHeight = 640;
    }

    public void InitializeView()
    {
        var notebook = new Gtk.Notebook();
        var resolvedWidgets = _presenter.GetResolvedWidgets();
        foreach (var (moduleName, widget) in resolvedWidgets)
        {
            var viewInstance = widget as IView;
            var tabLabel = new Gtk.Label { Label_ = moduleName };
            notebook.AppendPage(widget, tabLabel);
            viewInstance.InitializeView();
            viewInstance.ShowView();
            
        }

        SetChild(notebook); 
    }

    public void ShowView()
    {
        Show();
    }

    public void CloseView()
    {
        Destroy();
    }
}