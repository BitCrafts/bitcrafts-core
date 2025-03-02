using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Abstraction.Presenters;

public interface IUsersPresenter : IPresenter
{
    Task SaveUserAsync();
}