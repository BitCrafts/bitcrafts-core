using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Users.Abstraction.Entities;

namespace BitCrafts.Users.Abstraction.Views;

public interface ICreateUserView : IView
{
    event EventHandler<User> SaveClicked;
    event EventHandler CloseClicked;
    
    string GetPassword();
}