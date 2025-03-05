using BitCrafts.Infrastructure.Abstraction.Application.Managers;
using BitCrafts.Infrastructure.Abstraction.Application.Presenters;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Presenters;
using BitCrafts.Users.Abstraction.UseCases;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;
using BitCrafts.Users.Views;
using Microsoft.Extensions.DependencyInjection;

namespace BitCrafts.Users.Presenters;

public class UsersPresenter : BasePresenter<IUsersView>, IUsersPresenter
{
    public UsersPresenter(IServiceProvider serviceProvider) : base("Users", serviceProvider)
    {
        InitializeViewEvents();
    }

    protected override void Dispose(bool disposing)
    {
        //if (disposing) _serviceScope.Dispose();
        base.Dispose(disposing);
    }

    public async Task SaveUserAsync()
    {
   
        var user = new User()
        {
            LastName = "Test",
            FirstName = "Test",
            Email = "test@test.com",
            NationalNumber = "123456",
            PassportNumber = "123456",
            PhoneNumber = "123456",
            BirthDate = DateTime.Now
        };
        var request = new UserEventRequest { User = user, Password = "test" };
        await ServiceProvider.GetRequiredService<ICreateUserUseCase>()
            .ExecuteAsync(request);
    }

    private void InitializeViewEvents()
    {
        ((IUsersView)View).SaveClicked += async (_, _) => await SaveUserAsync();

        ((IUsersView)View).CloseClicked += (_, _) =>
            ServiceProvider.GetRequiredService<IWorkspaceManager>().ClosePresenter(this);
    }

    public async Task UpdateUserAsync()
    {
        var view = View as UsersView;
        if (view != null)
        {
            var user = view.GetUser();
            ValidateUser(user);

            await ServiceProvider.GetRequiredService<IUpdateUserUseCase>()
                .ExecuteAsync(new UserEventRequest { User = user });
        }
    }

    public void CancelEditing()
    {
        // Notify or reload the view data (implementation left as is)
    }

    private void ValidateUser(IUser user)
    {
        if (string.IsNullOrWhiteSpace(user.FirstName))
            throw new ArgumentException("First name is required.");

        if (string.IsNullOrWhiteSpace(user.LastName))
            throw new ArgumentException("Last name is required.");

        if (!user.Email.Contains("@"))
            throw new ArgumentException("Invalid email.");
    }

    protected override void OnViewLoaded(object sender, EventArgs e)
    {
        base.OnViewLoaded(sender, e);
        var lstUsers = new List<IUser>();
        for (int i = 1; i <= 10; i++)
        {
            lstUsers.Add(new User()
            {
                FirstName = $"Prénom{i}",
                LastName = $"Nom{i}",
                Email = $"user{i}@example.com",
                PhoneNumber = $"123456789{i}",
                NationalNumber = $"NN{i}",
                PassportNumber = $"PP{i}",
                BirthDate = DateTime.Now.AddYears(-20 - i),
            });
        }

        GetView<IUsersView>().RefreshUsers(lstUsers);
    }
}