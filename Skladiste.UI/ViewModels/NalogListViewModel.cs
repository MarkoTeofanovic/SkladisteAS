using System.Collections.ObjectModel;
using Skladiste.Nalozi;
using Skladiste.Proizvodi;
using Skladiste.UI.Views;

namespace Skladiste.UI.ViewModels;

// Use case: pregled naloga, rad sa povezanim entitetima (nalog - stavke - proizvod - korisnik)
public class NalogListViewModel : ViewModelBase
{
    private readonly NalogService _nalogService;
    private readonly ProizvodService _proizvodService;

    public ObservableCollection<Nalog> Nalozi { get; } = new();

    public RelayCommand NoviNalogCommand { get; }

    public NalogListViewModel(NalogService nalogService, ProizvodService proizvodService)
    {
        _nalogService = nalogService;
        _proizvodService = proizvodService;

        NoviNalogCommand = new RelayCommand(_ => NoviNalog());
        Ucitaj();
    }

    private void Ucitaj()
    {
        Nalozi.Clear();
        foreach (var nalog in _nalogService.PregledSvih())
            Nalozi.Add(nalog);
    }

    private void NoviNalog()
    {
        var viewModel = new NalogDetaljiViewModel(_nalogService, _proizvodService);
        var prozor = new NalogDetaljiWindow(viewModel);
        if (prozor.ShowDialog() == true)
            Ucitaj();
    }
}
