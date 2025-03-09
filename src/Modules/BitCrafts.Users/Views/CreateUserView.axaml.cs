using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using BitCrafts.Infrastructure.Abstraction.Events;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Events;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Views;

public partial class CreateUserView : BaseView, ICreateUserView
{
    private readonly IEventAggregator _eventAggregator;

    public CreateUserView()
    {
        InitializeComponent();
    }

    public CreateUserView(IEventAggregator eventAggregator) : this()
    {
        _eventAggregator = eventAggregator;
        IsModal = true;
    }

    public override void UnsetBusy()
    {
        LoadingOverlay.IsVisible = false;
        base.UnsetBusy();
    }

    public override void SetBusy(string message)
    {
        LoadingOverlay.IsVisible = true;
        base.SetBusy(message);
    }

    public string GetPassword()
    {
        return PasswordTextBox.Text != null ? PasswordTextBox.Text.Trim() : string.Empty;
    }

    private void CloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        _eventAggregator.Publish(new CreateUserPresenterCloseEvent());
    }

    private void AddButtonOnClick(object sender, RoutedEventArgs e)
    {
        _eventAggregator.Publish(new CreateUserClickEvent(GetUser(), GetPassword()));
    }

    private User GetUser()
    {
        var user = new User
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Email = EmailTextBox.Text,
            PhoneNumber = PhoneNumberTextBox.Text,
            BirthDate = BirthDatePicker.SelectedDate.HasValue ? BirthDatePicker.SelectedDate.Value : default,
            NationalNumber = NationalNumberTextBox.Text,
            PassportNumber = PassportNumberTextBox.Text,
            UserAccount = new UserAccount()
        };
        return user;
    }
}