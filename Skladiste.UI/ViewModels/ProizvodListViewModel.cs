using System.Collections.ObjectModel;
using System.Windows;
using Skladiste.Kategorije;
using Skladiste.Proizvodi;
using Skladiste.UI.Views;

namespace Skladiste.UI.ViewModels;

// Use case: pregled proizvoda
public class ProizvodListViewModel : ViewModelBase
{
    private readonly ProizvodService _proizvodService;
    private readonly KategorijaService _kategorijaService;

    public ObservableCollection<Proizvod> Proizvodi { get; } = new();

    private Proizvod? _izabran;
    public Proizvod? Izabran
    {
        get => _izabran;
        set => SetField(ref _izabran, value);
    }

    public RelayCommand DodajCommand { get; }
    public RelayCommand IzmeniCommand { get; }
    public RelayCommand ObrisiCommand { get; }

    public ProizvodListViewModel(ProizvodService proizvodService, KategorijaService kategorijaService)
    {
        _proizvodService = proizvodService;
        _kategorijaService = kategorijaService;

        DodajCommand = new RelayCommand(_ => OtvoriDijalog(null));
        IzmeniCommand = new RelayCommand(_ => OtvoriDijalog(Izabran), _ => Izabran != null);
        ObrisiCommand = new RelayCommand(_ => Obrisi(), _ => Izabran != null);

        Ucitaj();
    }

    private void Ucitaj()
    {
        Proizvodi.Clear();
        foreach (var proizvod in _proizvodService.PregledSvih())
            Proizvodi.Add(proizvod);
    }

    // Use case: kreiranje / izmena proizvoda
    private void OtvoriDijalog(Proizvod? postojeci)
    {
        var viewModel = new ProizvodDetaljiViewModel(_proizvodService, _kategorijaService, postojeci);
        var prozor = new ProizvodDetaljiWindow(viewModel);
        if (prozor.ShowDialog() == true)
            Ucitaj();
    }

    private void Obrisi()
    {
        if (Izabran == null)
            return;

        var potvrda = MessageBox.Show($"Obrisati proizvod '{Izabran.Naziv}'?", "Potvrda",
            MessageBoxButton.YesNo, MessageBoxImage.Question);
        if (potvrda != MessageBoxResult.Yes)
            return;

        _proizvodService.Obrisi(Izabran.Id);
        Ucitaj();
    }
}
