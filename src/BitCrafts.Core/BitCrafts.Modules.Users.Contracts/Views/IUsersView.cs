using BitCrafts.Core.Contracts.Views;
using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Contracts.Views;

public interface IUsersView : IView
{
    void DisplayUsers(IEnumerable<IUserEntity> users);
    event EventHandler UserAdded;
    event EventHandler<int> UserRemoved;
    event EventHandler OnLoaded;
    event EventHandler<int> UserSelected;
}