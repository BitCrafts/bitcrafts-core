using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Infrastructure.Abstraction.Threading;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;
using BitCrafts.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.Presenters;

public sealed class UsersPresenter : BasePresenter<IUsersView>, IUsersPresenter
{
    private IDisplayUsersUseCase _displayUsersUseCase;

    public UsersPresenter(IServiceProvider serviceProvider) : base("Users", serviceProvider)
    {
        _displayUsersUseCase = ServiceProvider.GetRequiredService<IDisplayUsersUseCase>();
    }

    public async Task SaveUserAsync()
    {
        await WindowManager.ShowDialogWindowAsync<ICreateUserPresenter>();
    }

    private void OnCreateUser(UserEventResponse arg)
    {
        View.AppendUser(arg.User);
    }

    protected override async void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.SetBusy("Loading users...");
        var requestEvent = new DisplayUsersEventRequest();
        var response = await _displayUsersUseCase.Execute(requestEvent);
        View.UnsetBusy();
        View.RefreshUsers(response.Users);
    }

    protected override void OnInitialize()
    {
        View.SaveClicked += async (_, _) => await SaveUserAsync();
        View.CloseClicked += (_, _) =>
            ServiceProvider.GetRequiredService<IWorkspaceManager>().ClosePresenter<IUsersPresenter>();
        ServiceProvider.GetRequiredService<IEventAggregator>().Subscribe<UserEventResponse>(OnCreateUser);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ServiceProvider.GetRequiredService<IEventAggregator>().Unsubscribe<UserEventResponse>(OnCreateUser);
        }

        base.Dispose(disposing);
    }
}