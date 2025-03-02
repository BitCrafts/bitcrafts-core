using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.Presenters;

public class UsersPresenter : BasePresenter<IUsersView>, IUsersPresenter
{
    public UsersPresenter(IServiceProvider serviceProvider) : base("Users", serviceProvider)
    {
        InitializeViewEvents();
    }

    public async Task SaveUserAsync()
    {
        var user = ((IUsersView)View).GetUser();
        ValidateUser(user);

        await ServiceProvider.GetRequiredService<ICreateUserUseCase>()
            .ExecuteAsync(new UserEventRequest { User = user });
    }

    private void InitializeViewEvents()
    {
        ((IUsersView)View).SaveClicked += async (_, _) => await SaveUserAsync();
        ((IUsersView)View).UpdateClicked += async (_, _) => await UpdateUserAsync();
        ((IUsersView)View).CancelClicked += (_, _) => CancelEditing();
        ((IUsersView)View).CloseClicked += (_, _) =>
            ServiceProvider.GetRequiredService<IWorkspaceManager>().ClosePresenterAsync(this.GetType());
    }

    public async Task UpdateUserAsync()
    {
        var user = View.GetUser();
        ValidateUser(user);

        await ServiceProvider.GetRequiredService<IUpdateUserUseCase>()
            .ExecuteAsync(new UserEventRequest { User = user });
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

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
    }
}