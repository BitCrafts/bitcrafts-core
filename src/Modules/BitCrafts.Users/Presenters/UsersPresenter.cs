using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Presenters;

public class UsersPresenter : BasePresenter<IUsersView>, IUsersPresenter
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly IUpdateUserUseCase _updateUserUseCase;


    public override async Task InitializeAsync()
    {
        await base.InitializeAsync();
        InitializeViewEvents();
    }

    public UsersPresenter(IServiceProvider serviceProvider, ICreateUserUseCase createUserUseCase,
        IUpdateUserUseCase updateUserUseCase) : base(serviceProvider)
    {
        _createUserUseCase = createUserUseCase;
        _updateUserUseCase = updateUserUseCase;
    }

    public async Task SaveUserAsync()
    {
        var user = ((IUsersView)View).GetUser();
        ValidateUser(user);

        await _createUserUseCase.ExecuteAsync(new UserEventRequest { User = user });
    }

    private void InitializeViewEvents()
    {
        ((IUsersView)View).SaveClicked += async (_, _) => await SaveUserAsync();
        ((IUsersView)View).UpdateClicked += async (_, _) => await UpdateUserAsync();
        ((IUsersView)View).CancelClicked += (_, _) => CancelEditing();
    }

    public async Task UpdateUserAsync()
    {
        var user = View.GetUser();
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

    protected override void OnWindowClosed(object sender, EventArgs e)
    {
    }

    protected override void OnWindowLoaded(object sender, EventArgs e)
    {
    }
}