using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.Presenters;

public sealed class UsersPresenter : BasePresenter<IUsersView>, IUsersPresenter
{
    private readonly IDisplayUsersUseCase _displayUsersUseCase;
    private readonly IEventAggregator _eventAggregator;
    private readonly IWindowManager _windowManager;
    private readonly IWorkspaceManager _workspaceManager;

    public UsersPresenter(IDisplayUsersUseCase displayUsersUseCase, IWindowManager windowManager, IUsersView view
        , ILogger<UsersPresenter> logger, IWorkspaceManager workspaceManager,
        IEventAggregator eventAggregator) : base("Users", view, logger)
    {
        _windowManager = windowManager;
        _workspaceManager = workspaceManager;
        _eventAggregator = eventAggregator;
        _displayUsersUseCase = displayUsersUseCase;
    }

    public async Task SaveUserAsync()
    {
        await _windowManager.ShowDialogWindowAsync<ICreateUserPresenter>();
    }

    private void OnCreateUser(CreateUserEventResponse arg)
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
            _workspaceManager.ClosePresenter<IUsersPresenter>();
        _eventAggregator.Subscribe<CreateUserEventResponse>(OnCreateUser);
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing) _eventAggregator.Unsubscribe<CreateUserEventResponse>(OnCreateUser);

        base.Dispose(disposing);
    }
}