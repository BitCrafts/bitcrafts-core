using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using Microsoft.Extensions.Logging;

namespace BitCrafts.Users.Presenters;

public class CreateUserPresenter : BasePresenter<ICreateUserView>, ICreateUserPresenter
{
    private readonly ICreateUserUseCase _createUserUseCase;
    private readonly IEventAggregator _eventAggregator;
    private readonly IWindowManager _windowManager;

    public CreateUserPresenter(ICreateUserView view, ICreateUserUseCase createUserUseCase,
        IEventAggregator eventAggregator, IWindowManager windowManager,
        ILogger<CreateUserPresenter> logger) : base("New User", view,
        logger)
    {
        _createUserUseCase = createUserUseCase;
        _eventAggregator = eventAggregator;
        _windowManager = windowManager;
    }

    protected override void OnInitialize()
    {
        _eventAggregator.Subscribe<CreateUserClickEvent>(OnCreateUserClick);
        _eventAggregator.Subscribe<CreateUserPresenterCloseEvent>(OnClose);
    }

    private void OnClose(CreateUserPresenterCloseEvent obj)
    {
        _windowManager.CloseWindow<CreateUserPresenter>();
    }

    private async void OnCreateUserClick(CreateUserClickEvent obj)
    {
        var useCaseInput = new CreateUserUseCaseInput
        {
            User = obj.User,
            Password = obj.Password
        };
        View.SetBusy("Loading...");
        await _createUserUseCase.Execute(useCaseInput);
        View.UnsetBusy();
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _eventAggregator.Unsubscribe<CreateUserClickEvent>(OnCreateUserClick);
            _eventAggregator.Unsubscribe<CreateUserPresenterCloseEvent>(OnClose);
        }

        base.Dispose(disposing);
    }
}