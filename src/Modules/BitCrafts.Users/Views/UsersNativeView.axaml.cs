using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using BitCrafts.Users.Abstraction.Entities;
using BitCrafts.Users.Entities;

namespace BitCrafts.Users.Views;

public partial class UsersNativeView : UserControl
{
    private IUser _currentUser = new User();

    public UsersNativeView()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void SaveButton_OnClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        // Mettre à jour les données de l'utilisateur à partir des champs du UI
        _currentUser.FirstName = this.FindControl<TextBox>("FirstNameTextBox")?.Text;
        _currentUser.LastName = this.FindControl<TextBox>("LastNameTextBox")?.Text;
        _currentUser.Email = this.FindControl<TextBox>("EmailTextBox")?.Text;
        _currentUser.PhoneNumber = this.FindControl<TextBox>("PhoneNumberTextBox").Text;
        _currentUser.BirthDate =
            this.FindControl<CalendarDatePicker>("BirthDatePicker").SelectedDate.GetValueOrDefault();
        _currentUser.NationalNumber = this.FindControl<TextBox>("NationalNumberTextBox").Text;
        _currentUser.PassportNumber = this.FindControl<TextBox>("PassportNumberTextBox").Text;
    }

    /* private void LoadUser(User user)
     {
         FirstNameTextBox.Text = user.FirstName;
         LastNameTextBox.Text = user.LastName;
         EmailTextBox.Text = user.Email;
         PhoneNumberTextBox.Text = user.PhoneNumber;
         BirthDatePicker.SelectedDate = user.BirthDate;
         NationalNumberTextBox.Text = user.NationalNumber;
         PassportNumberTextBox.Text = user.PassportNumber;
     }*/

    private void CancelButton_OnClick(object sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
    }
}