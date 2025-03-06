using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Views;

public interface IUsersView : IView
{
    event EventHandler SaveClicked; 
    event EventHandler CloseClicked;  
    void RefreshUsers(IEnumerable<User> users);
    void AppendUser(User user);
}