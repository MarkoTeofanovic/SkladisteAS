using System.Windows;
using Skladiste.UI.ViewModels;

namespace Skladiste.UI.Views;

// View: XAML, bez code-behind logike osim inicijalizacije
public partial class LoginWindow : Window
{
    public LoginWindow(LoginViewModel viewModel)
    {
        InitializeComponent();

        viewModel.ZatvoriProzor += () =>
        {
            DialogResult = viewModel.Uspesno;
            Close();
        };
        DataContext = viewModel;
    }
}
