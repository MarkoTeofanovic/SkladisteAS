using System.Windows;
using Skladiste.UI.ViewModels;

namespace Skladiste.UI;

// View: XAML, bez code-behind logike osim inicijalizacije
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}
