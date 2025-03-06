using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.Presenters;

public class CreateUserPresenter : BasePresenter<ICreateUserView>, ICreateUserPresenter
{
    private ICreateUserView _view;

    public CreateUserPresenter(IServiceProvider serviceProvider) : base("New User", serviceProvider)
    {
        Initialize();
    }

    private void Initialize()
    {
        _view = GetView<ICreateUserView>();
        _view.SaveClicked += OnSaveClicked;
        _view.CloseClicked += OnCloseClicked;
    }

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _view.SaveClicked -= OnSaveClicked;
            _view.CloseClicked -= OnCloseClicked;
        }

        base.Dispose(disposing);
    }

    private void OnCloseClicked(object sender, EventArgs e)
    {
        ServiceProvider.GetRequiredService<IWindowManager>().CloseWindow<ICreateUserPresenter>();
    }

    private async void OnSaveClicked(object sender, User e)
    {
        var userEvent = new UserEventRequest()
        {
            User = e,
            Password = _view.GetPassword(),
        };
        await ServiceProvider.GetRequiredService<ICreateUserUseCase>().ExecuteAsync(userEvent);
    }
}