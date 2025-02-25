using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Abstraction.Presenters;

public interface IUsersPresenter : IPresenter<IUsersView>
{
    Task SaveUserAsync();
}