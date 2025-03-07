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
    public CreateUserPresenter(IServiceProvider serviceProvider) : base("New User", serviceProvider)
    {
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
        ServiceProvider.GetRequiredService<IWindowManager>().CloseWindow(this);
    }

    private void OnSaveClicked(object sender, User e)
    {
        var userEvent = new UserEventRequest()
        {
            User = e,
            Password = View.GetPassword(),
        };
        var response = ServiceProvider.GetRequiredService<ICreateUserUseCase>().Execute(userEvent);
        ServiceProvider.GetRequiredService<IEventAggregator>().Publish<UserEventResponse>(response);
    }
}