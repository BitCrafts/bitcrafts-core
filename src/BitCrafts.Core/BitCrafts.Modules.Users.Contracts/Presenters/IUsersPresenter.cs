using BitCrafts.Core.Contracts.Applications.Presenters;

namespace BitCrafts.Modules.Users.Contracts.Presenters;

public interface IUsersPresenter : IPresenter
{
    void AddUser();
}