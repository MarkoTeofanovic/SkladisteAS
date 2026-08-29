using System.Windows;
using Skladiste.UI.ViewModels;

namespace Skladiste.UI.Views;

// View: XAML, bez code-behind logike osim inicijalizacije
public partial class NalogDetaljiWindow : Window
{
    public NalogDetaljiWindow(NalogDetaljiViewModel viewModel)
    {
        InitializeComponent();

        viewModel.ZatvoriProzor += () =>
        {
            DialogResult = viewModel.Sacuvano;
            Close();
        };
        DataContext = viewModel;
    }
}
