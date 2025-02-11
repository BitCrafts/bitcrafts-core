using BitCrafts.Core.Contracts.Applications.Presenters;
using BitCrafts.Modules.Users.Contracts.Views;

namespace BitCrafts.Modules.Users.Contracts.Presenters;

public interface IUsersPresenter : IPresenter<IUsersView>
{
    void LoadUsers();
    void AddUser();

}