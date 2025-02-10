using BitCrafts.Modules.Users.Contracts.Presenters;
using BitCrafts.Modules.Users.Contracts.Views;
using Gtk;

namespace BitCrafts.Modules.Users.Views;

public class UsersView : Box, IUsersView
{
    private IUsersPresenter _presenter;
    public event EventHandler OnViewLoaded;
    public event EventHandler OnViewClosed;
    private Button _addButton;


    public UsersView(IUsersPresenter presenter)
    {
        _presenter = presenter;
        this.OnShow += OnOnShow;
        this.OnDestroy += OnOnDestroy;
        BuildView();
    }

    private void OnOnDestroy(Widget sender, EventArgs args)
    {
        OnViewClosed?.Invoke(this, args);
        this.OnShow -= OnOnShow;
        this.OnDestroy -= OnOnDestroy;
    }

    private void OnOnShow(Widget sender, EventArgs args)
    {
        OnViewLoaded?.Invoke(this, args);
    }

    private void BuildView()
    {
        // Création et configuration du bouton "Ajouter Utilisateur"
        _addButton = new Button();
        _addButton.Label = "Ajouter Utilisateur";
        _addButton.OnClicked += (sender, e) => OnAddUser?.Invoke(this, e);

        Append(_addButton);
    }

    public void ShowView()
    {
        Show();
    }

    public void CloseView()
    {
        Hide();
    } 

    public event EventHandler OnAddUser;
}