using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Core.GtkApplication.Presenters;
using Gdk;
using Gtk;

namespace BitCrafts.Core.GtkApplication.Views;

public class MainView : Window, IMainView
{
    private readonly IMainPresenter _presenter;

    public MainView(IMainPresenter presenter)
    {
        _presenter = presenter;
        Title = "BitCrafts";
        DefaultWidth = 800;
        DefaultHeight = 640;
        BuildView(); 

    }

    private void BuildView()
    {
        
        // Création d'une boîte verticale comme conteneur principal
        var vbox = new Box();
        vbox.SetOrientation(orientation: Orientation.Vertical);
        var notebook = new Notebook(); 
        var label = new Label();
        label.Label_ = "\"Vous avez chargé la page principale\"";
        notebook.AppendPage(label, new Label(){Label_ = "Test"});
        SetChild(notebook);


    }
    public void ShowView()
    {
        throw new NotImplementedException();
    }

    public void CloseView()
    {
        throw new NotImplementedException();
    }

    public event EventHandler OnViewLoaded;
    public event EventHandler OnViewClosed;
}