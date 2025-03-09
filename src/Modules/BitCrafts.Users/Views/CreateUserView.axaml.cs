using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Views;

namespace BitCrafts.Users.Views;

public partial class CreateUserView : BaseView, ICreateUserView
{
    public CreateUserView()
    {
        IsModal = true;
        InitializeComponent();
        AddButton.Click += AddButtonOnClick;
        CloseButton.Click += CloseButtonOnClick;
    }

    public event EventHandler<User> SaveClicked;
    public event EventHandler CloseClicked;

    public string GetPassword()
    {
        return PasswordTextBox.Text != null ? PasswordTextBox.Text.Trim() : string.Empty;
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

    private void CloseButtonOnClick(object sender, RoutedEventArgs e)
    {
        CloseClicked?.Invoke(this, EventArgs.Empty);
    }

    private void AddButtonOnClick(object sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, GetUser());
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

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            AddButton.Click -= AddButtonOnClick;
            CloseButton.Click -= CloseButtonOnClick;
        }

        base.Dispose(disposing);
    }
}