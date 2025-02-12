using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Core.Presenters;
using Gtk;
using Gtk.Internal;
using ApplicationWindow = Gtk.ApplicationWindow;
using Builder = Gtk.Builder;
using Label = Gtk.Label;
using Notebook = Gtk.Notebook;

namespace BitCrafts.Core.Views;

public class MainWindowView : ApplicationWindow, IMainWindowView
{
    private readonly IMainPresenter _presenter;
    [Connect] private readonly Notebook mainNotebook;

    private MainWindowView(Builder builder)
        : base(new ApplicationWindowHandle(builder.GetPointer("mainApplication"), false))
    {
        builder.Connect(this);
        DefaultWidth = 800;
        DefaultHeight = 640;
    }

    public MainWindowView(IMainPresenter presenter) :
        this(new Builder("MainApplication.glade"))
    {
        _presenter = presenter;
        _presenter.SetView(this);
    }

    public void InitializeView()
    {
        var resolvedWidgets = _presenter.GetResolvedWidgets();
        foreach (var (moduleName, widget) in resolvedWidgets)
        {
            var tabLabel = new Label { Label_ = moduleName };
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