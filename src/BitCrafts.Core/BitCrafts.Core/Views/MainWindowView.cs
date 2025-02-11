using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Core.Presenters;

namespace BitCrafts.Core.Views;

public class MainWindowView : Gtk.ApplicationWindow, IMainWindowView
{
    private readonly IMainPresenter _presenter;
    [Gtk.Connect] private readonly Gtk.Notebook mainNotebook;

    private MainWindowView(Gtk.Builder builder)
        : base(new Gtk.Internal.ApplicationWindowHandle(builder.GetPointer("mainApplication"), false))
    {
        builder.Connect(this);
        DefaultWidth = 800;
        DefaultHeight = 640;
    }

    public MainWindowView(IMainPresenter presenter) :
        this(new Gtk.Builder("MainApplication.glade"))
    {
        _presenter = presenter;
        _presenter.SetView(this);
    }

    public void InitializeView()
    {
        var resolvedWidgets = _presenter.GetResolvedWidgets();
        foreach (var (moduleName, widget) in resolvedWidgets)
        {
            var tabLabel = new Gtk.Label { Label_ = moduleName };
            mainNotebook.AppendPage(widget, tabLabel);
            if (widget is IView viewInstance)
            {
                viewInstance.InitializeView();
                viewInstance.ShowView();
            }
        }
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