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
    private IUsersView _view;

    public UsersPresenter(IServiceProvider serviceProvider) : base("Users", serviceProvider)
    {
        InitializeViewEvents();
    }

    public Task SaveUserAsync()
    {
        ServiceProvider.GetRequiredService<IWindowManager>().ShowWindow<ICreateUserPresenter>(true);
        return Task.CompletedTask;
    }

    private void InitializeViewEvents()
    {
        _view = GetView<IUsersView>();
        _view.SaveClicked += async (_, _) => await SaveUserAsync();
        _view.CloseClicked += (_, _) => ServiceProvider.GetRequiredService<IWorkspaceManager>().ClosePresenter(this);
        ServiceProvider.GetRequiredService<IEventAggregator>().Subscribe<DisplayUsersEventResponse>(OnDisplay);
        ServiceProvider.GetRequiredService<IEventAggregator>().Subscribe<UserEventResponse>(OnCreateUser);
    }

    private Task OnCreateUser(UserEventResponse arg)
    {
        _view.AppendUser(arg.User);
        return Task.CompletedTask;
    }

    private Task OnDisplay(DisplayUsersEventResponse arg)
    {
        _view.RefreshUsers(arg.Users);
        return Task.CompletedTask;
    }


    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        ServiceProvider.GetRequiredService<IDisplayUsersUseCase>().ExecuteAsync(new DisplayUsersEventRequest());
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            ServiceProvider.GetRequiredService<IEventAggregator>().Unsubscribe<DisplayUsersEventResponse>(OnDisplay);
            ServiceProvider.GetRequiredService<IEventAggregator>().Unsubscribe<UserEventResponse>(OnCreateUser);
        }

        base.Dispose(disposing);
    }
}