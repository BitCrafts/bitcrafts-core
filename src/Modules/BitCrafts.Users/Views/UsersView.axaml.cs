using System.Collections.ObjectModel;
using Avalonia.Interactivity;
using BitCrafts.Infrastructure.Abstraction.Avalonia.Views;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;

namespace BitCrafts.Users.Views;

public partial class UsersView : BaseView, IUsersView
{
    public event EventHandler SaveClicked;
    public event EventHandler CloseClicked;
    private ObservableCollection<User> _users = new ObservableCollection<User>();

    public void RefreshUsers(IEnumerable<User> users)
    {
        _users.Clear();
        foreach (var user in users)
        {
            _users.Add(user);
        }

        UsersDataGrid.ItemsSource = _users;
    }

    public UsersView(IServiceProvider serviceProvider)
        : base(serviceProvider)
    {
        InitializeComponent();
    }

    public void AppendUser(User user)
    {
        _users.Add(user);
    }
    public void SetUser(User user)
    {
        FirstNameTextBox.Text = user.FirstName;
        LastNameTextBox.Text = user.LastName;
        EmailTextBox.Text = user.Email;
        PhoneNumberTextBox.Text = user.PhoneNumber;
        BirthDatePicker.SelectedDate = user.BirthDate;
        NationalNumberTextBox.Text = user.NationalNumber;
        PassportNumberTextBox.Text = user.PassportNumber;
    }

    public User GetUser()
    {
        return new User
        {
            FirstName = FirstNameTextBox.Text,
            LastName = LastNameTextBox.Text,
            Email = EmailTextBox.Text,
            PhoneNumber = PhoneNumberTextBox.Text,
            BirthDate = BirthDatePicker.SelectedDate.GetValueOrDefault(),
            NationalNumber = NationalNumberTextBox.Text,
            PassportNumber = PassportNumberTextBox.Text
        };
    }

    private void SaveButton_OnClick(object sender, RoutedEventArgs e)
    {
        SaveClicked?.Invoke(this, EventArgs.Empty);
    }


    private void DataGrid_AddUser(User user)
    {
        _users.Add(user);
        UsersDataGrid.ItemsSource = _users;
    }


    public void Initialize()
    {
        DataGrid_AddUser(new User()
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = DateTime.Today,
            NationalNumber = "123456",
            PassportNumber = "123456",
            Email = "john.doe@example.com",
            PhoneNumber = "123456"
        });
    }

    private void Closebutton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseClicked?.Invoke(this, EventArgs.Empty);
    }
}