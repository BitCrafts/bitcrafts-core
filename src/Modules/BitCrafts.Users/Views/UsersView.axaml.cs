using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Abstraction.Views;
using BitCrafts.Users.Entities;

namespace BitCrafts.Users.Views;

public partial class UsersView : UserControl, IUsersView
{
    public UsersView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public event EventHandler SaveClicked;
    public event EventHandler UpdateClicked;
    public event EventHandler CancelClicked;

    public void SetUser(IUser user)
    {
        this.FindControl<TextBox>("FirstNameTextBox").Text = user.FirstName;
        this.FindControl<TextBox>("LastNameTextBox").Text = user.LastName;
        this.FindControl<TextBox>("EmailTextBox").Text = user.Email;
        this.FindControl<TextBox>("PhoneNumberTextBox").Text = user.PhoneNumber;
        this.FindControl<CalendarDatePicker>("BirthDatePicker").SelectedDate = user.BirthDate;
        this.FindControl<TextBox>("NationalNumberTextBox").Text = user.NationalNumber;
        this.FindControl<TextBox>("PassportNumberTextBox").Text = user.PassportNumber;
    }

    public IUser GetUser()
    {
        return new User
        {
            FirstName = this.FindControl<TextBox>("FirstNameTextBox").Text,
            LastName = this.FindControl<TextBox>("LastNameTextBox").Text,
            Email = this.FindControl<TextBox>("EmailTextBox").Text,
            PhoneNumber = this.FindControl<TextBox>("PhoneNumberTextBox").Text,
            BirthDate = this.FindControl<CalendarDatePicker>("BirthDatePicker").SelectedDate.GetValueOrDefault(),
            NationalNumber = this.FindControl<TextBox>("NationalNumberTextBox").Text,
            PassportNumber = this.FindControl<TextBox>("PassportNumberTextBox").Text
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
}