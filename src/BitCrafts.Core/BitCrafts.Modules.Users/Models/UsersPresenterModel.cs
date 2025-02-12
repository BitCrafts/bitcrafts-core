using BitCrafts.Modules.Users.Contracts.Models;

namespace BitCrafts.Modules.Users.Models;

public class UsersPresenterModel : IUsersPresenterModel
{
    public List<IUserEntity> Users { get; set; }
    public IUserEntity SelectedUser { get; set; }

    public UsersPresenterModel()
    {
        Users = new List<IUserEntity>();
    }
}