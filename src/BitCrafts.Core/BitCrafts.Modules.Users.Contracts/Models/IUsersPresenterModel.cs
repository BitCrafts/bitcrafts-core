namespace BitCrafts.Modules.Users.Contracts.Models;

public interface IUsersPresenterModel
{
    List<IUserEntity> Users { get; set; }
    IUserEntity SelectedUser { get; set; }
    
}