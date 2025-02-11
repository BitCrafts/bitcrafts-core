using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Contracts.Views;

public interface IUsersView : IView
{
    void DisplayUsers(IList<IUserModel> users);
    event EventHandler UserAdded;
    event EventHandler<int> UserRemoved;
    event EventHandler OnLoaded;
    event EventHandler<int> UserSelected;
}