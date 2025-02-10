using BitCrafts.Core.Contracts.Applications.Views;
using BitCrafts.Modules.Users.Contracts.Presenters;

namespace BitCrafts.Modules.Users.Contracts.Views;

public interface IUsersView : IView
{
    event EventHandler OnAddUser;
}