using BitCrafts.Infrastructure.Abstraction.Application.UI;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Views;

public interface IUsersView : IView
{
    event EventHandler SaveClicked;
    event EventHandler CancelClicked;
    event EventHandler UpdateClicked;
    void SetUser(IUser user);
    IUser GetUser();
}