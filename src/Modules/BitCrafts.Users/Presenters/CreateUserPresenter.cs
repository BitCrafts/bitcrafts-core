using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using Microsoft.Extensions.DependencyInjection;
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
        View.SaveClicked += OnSaveClicked;
        View.CloseClicked += OnCloseClicked;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            View.SaveClicked -= OnSaveClicked;
            View.CloseClicked -= OnCloseClicked;
        }

        base.Dispose(disposing);
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        _windowManager.CloseWindow<ICreateUserPresenter>();
    }

    private async void OnSaveClicked(object sender, User e)
    {
        var userEvent = new UserEventRequest()
        {
            User = e,
            Password = View.GetPassword(),
        };
        View.SetBusy("Loading...");
        await Task.Delay(2000);
        var response = await _createUserUseCase.Execute(userEvent);
        View.UnsetBusy();
        _eventAggregator.Publish(response);
    }
}