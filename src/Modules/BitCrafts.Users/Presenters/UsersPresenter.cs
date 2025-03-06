using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
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
    public UsersPresenter(IServiceProvider serviceProvider) : base("Users", serviceProvider)
    {
    }

    public Task SaveUserAsync()
    {
        var createUserPresenter = ServiceProvider.GetRequiredService<ICreateUserPresenter>();
        ServiceProvider.GetRequiredService<IWindowManager>().ShowWindow(createUserPresenter, true);
        return Task.CompletedTask;
    }

    private Task OnCreateUser(UserEventResponse arg)
    {
        View.AppendUser(arg.User);
        return Task.CompletedTask;
    }

    protected override async void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        var response = await ServiceProvider.GetRequiredService<IDisplayUsersUseCase>()
            .ExecuteAsync(new DisplayUsersEventRequest());
        View.RefreshUsers(response.Users);
    }

    protected override void OnInitialize()
    {
        View.SaveClicked += async (_, _) => await SaveUserAsync();
        View.CloseClicked += (_, _) => ServiceProvider.GetRequiredService<IWorkspaceManager>().ClosePresenter(this);
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