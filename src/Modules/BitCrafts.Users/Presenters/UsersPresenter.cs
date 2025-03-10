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
    private readonly IDeleteUserUseCase _deleteUserUseCase;
    private readonly IDisplayUsersUseCase _displayUsersUseCase;
    private readonly IEventAggregator _eventAggregator;
    private readonly IUpdateUserUseCase _updateUserUseCase;
    private readonly IWindowManager _windowManager;
    private readonly IWorkspaceManager _workspaceManager;

    public UsersPresenter(IDisplayUsersUseCase displayUsersUseCase,
        IUpdateUserUseCase updateUserUseCase, IDeleteUserUseCase deleteUserUseCase,
        IWindowManager windowManager, IUsersView view, ILogger<UsersPresenter> logger,
        IWorkspaceManager workspaceManager,
        IEventAggregator eventAggregator) : base("Users", view, logger)
    {
        _windowManager = windowManager;
        _workspaceManager = workspaceManager;
        _eventAggregator = eventAggregator;
        _displayUsersUseCase = displayUsersUseCase;
        _updateUserUseCase = updateUserUseCase;
        _deleteUserUseCase = deleteUserUseCase;
    }

    protected override async void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        View.SetBusy("Loading users...");
        await _displayUsersUseCase.Execute();
        View.UnsetBusy();
    }

    protected override void OnInitialize()
    {
        _eventAggregator.Subscribe<AddUserClickEvent>(OnAddUserClicked);
        _eventAggregator.Subscribe<DeleteUserClickEvent>(OnDeleteUserClicked);
        _eventAggregator.Subscribe<UpdateUserClickEvent>(OnUpdateUserClicked);
        _eventAggregator.Subscribe<UsersPresenterCloseEvent>(OnUsersPresenterClosed);
    }

    private void OnUsersPresenterClosed(UsersPresenterCloseEvent obj)
    {
        _workspaceManager.ClosePresenter<UsersPresenter>();
    }

    private async void OnUpdateUserClicked(UpdateUserClickEvent obj)
    {
        await _updateUserUseCase.Execute(new UpdateUserUseCaseInput(obj.User));
    }

    private async void OnDeleteUserClicked(DeleteUserClickEvent obj)
    {
        await _deleteUserUseCase.Execute(new DeleteUserUseCaseInput(obj.User));
    }

    private async void OnAddUserClicked(AddUserClickEvent obj)
    {
        await _windowManager.ShowDialogWindowAsync<ICreateUserPresenter>();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _eventAggregator.Unsubscribe<AddUserClickEvent>(OnAddUserClicked);
            _eventAggregator.Unsubscribe<DeleteUserClickEvent>(OnDeleteUserClicked);
            _eventAggregator.Unsubscribe<UpdateUserClickEvent>(OnUpdateUserClicked);
            _eventAggregator.Unsubscribe<UsersPresenterCloseEvent>(OnUsersPresenterClosed);
        }

        base.Dispose(disposing);
    }
}