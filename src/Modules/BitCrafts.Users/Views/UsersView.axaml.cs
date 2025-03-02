using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitCrafts.Infrastructure.Abstraction.Application.Views;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;

namespace BitCrafts.Users.Views;

public partial class UsersView : UserControl, IUsersView
{
    public event EventHandler SaveClicked;
    public event EventHandler UpdateClicked;
    public event EventHandler CancelClicked;
    public event EventHandler CloseClicked;
    public event EventHandler ViewLoadedEvent;
    public event EventHandler ViewClosedEvent;
    public bool IsWindow => false;
    public IView ParentView { get; set; }

    private ObservableCollection<User> _users = new ObservableCollection<User>();

    public UsersView()
    {
        InitializeComponent();
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        ViewClosedEvent?.Invoke(this, EventArgs.Empty);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewLoadedEvent?.Invoke(this, EventArgs.Empty);
        SetUser(new User()
        {
            FirstName = "John",
            LastName = "Doe",
            BirthDate = DateTime.Today,
            NationalNumber = "123456",
            PassportNumber = "123456",
            Email = "john.doe@example.com",
            PhoneNumber = "123456"
        });
        UsersDataGrid.ItemsSource = _users;
        UsersDataGrid.SelectionMode = DataGridSelectionMode.Single;
    }

    public void SetUser(IUser user)
    {
        FirstNameTextBox.Text = user.FirstName;
        LastNameTextBox.Text = user.LastName;
        EmailTextBox.Text = user.Email;
        PhoneNumberTextBox.Text = user.PhoneNumber;
        BirthDatePicker.SelectedDate = user.BirthDate;
        NationalNumberTextBox.Text = user.NationalNumber;
        PassportNumberTextBox.Text = user.PassportNumber;
    }

    public IUser GetUser()
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

    private void CancelButton_OnClick(object sender, RoutedEventArgs e)
    {
        CancelClicked?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
    {
        UpdateClicked?.Invoke(this, EventArgs.Empty);
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

    public void SetTitle(string title)
    {
    }

    public string GetTitle(string title)
    {
        return string.Empty;
    }

    public void Show()
    {
        IsVisible = true;
    }

    public void Hide()
    {
        IsVisible = false;
    }

    public void Dispose()
    {
        // TODO release managed resources here
    }

    private void Closebutton_OnClick(object sender, RoutedEventArgs e)
    {
        CloseClicked?.Invoke(this, EventArgs.Empty);
    }
}