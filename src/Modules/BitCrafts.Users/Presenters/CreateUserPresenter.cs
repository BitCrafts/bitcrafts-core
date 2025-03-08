using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.Presenters;

public class CreateUserPresenter : BasePresenter<ICreateUserView>, ICreateUserPresenter
{
    private ICreateUserUseCase _useCase;

    public CreateUserPresenter(IServiceProvider serviceProvider) : base("New User", serviceProvider)
    {
        _useCase = serviceProvider.GetService<ICreateUserUseCase>();
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
        WindowManager.CloseWindow<ICreateUserPresenter>();
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
        var response = await _useCase.Execute(userEvent);
        View.UnsetBusy();
        ServiceProvider.GetRequiredService<IEventAggregator>().Publish(response);
    }
}