using BitCrafts.Core.Contracts.Views; 
using Builder = Gtk.Builder;
using Label = Gtk.Label;
using Notebook = Gtk.Notebook;
using Widget = Gtk.Widget;

namespace BitCrafts.Core.Views;

public sealed class MainView : Gtk.ApplicationWindow, IMainView
{
    [Gtk.Connect] private readonly Notebook mainNotebook;

    private MainView(Builder builder)
        : base(new Gtk.Internal.ApplicationWindowHandle(builder.GetPointer("mainApplication"), false))
    {
        builder.Connect(this);
        DefaultWidth = 800;
        DefaultHeight = 640;
        base.OnShow += OnOnShow;
        base.OnDestroy += OnOnDestroy;
    }

    private void OnOnDestroy(Widget sender, EventArgs args)
    {
        Unloaded?.Invoke(this, EventArgs.Empty);
    }

    private void OnOnShow(Widget sender, EventArgs args)
    {
        Loaded?.Invoke(this, EventArgs.Empty);
    }

    public MainView() :
        this(new Builder("MainApplication.glade"))
    {
    }

    public void InitializeModules(List<(string moduleName, Widget widget)> modulesWidgets)
    {
        foreach (var (moduleName, widget) in modulesWidgets)
        {
            var tabLabel = new Label { Label_ = moduleName };
            mainNotebook.AppendPage(widget, tabLabel);
            if (widget is IView viewInstance)
            {
                viewInstance.Show();
            }
        }
    } 

    public event EventHandler<EventArgs> Loaded;
    public event EventHandler<EventArgs> Unloaded;
}