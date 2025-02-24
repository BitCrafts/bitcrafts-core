using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Presenters;

public class UsersPresenter : IUsersPresenter
{
    private readonly IUsersView _view;
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;

    public UsersPresenter(IUsersView view, ICreateUserUseCase createUserUseCase, IUpdateUserUseCase updateUserUseCase)
    {
        _view = view;
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
 
        InitializeViewEvents();
    }

    private void InitializeViewEvents()
    {
        _view.SaveClicked += async (_, _) => await SaveUserAsync();
        _view.UpdateClicked += async (_, _) => await UpdateUserAsync();
        _view.CancelClicked += (_, _) => CancelEditing();
    }

    public async Task SaveUserAsync()
    {
        var user = _view.GetUser();
        ValidateUser(user);

        await _createUserUseCase.ExecuteAsync(new UserEventRequest { User = user }); 
    }

    public async Task UpdateUserAsync()
    {
        var user = _view.GetUser();
        ValidateUser(user);

        await _updateUserUseCase.ExecuteAsync(new UserEventRequest { User = user }); 
    }

    public void CancelEditing()
    {
        // Notify or reload the view data (implementation left as is)
    }

    private void ValidateUser(IUser user)
    {
        if (string.IsNullOrWhiteSpace(user.FirstName))
            throw new ArgumentException("First name is required.");

        if (string.IsNullOrWhiteSpace(user.LastName))
            throw new ArgumentException("Last name is required.");

        if (!user.Email.Contains("@"))
            throw new ArgumentException("Invalid email."); 
    }

}