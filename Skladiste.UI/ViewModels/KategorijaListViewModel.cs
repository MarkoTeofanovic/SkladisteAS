using System.Collections.ObjectModel;
using Skladiste.Kategorije;

namespace Skladiste.UI.ViewModels;

// Use case: kreiranje i pregled kategorija
public class KategorijaListViewModel : ViewModelBase
{
    private readonly KategorijaService _kategorijaService;

    public ObservableCollection<Kategorija> Kategorije { get; } = new();

    private string _noviNaziv = string.Empty;
    public string NoviNaziv
    {
        get => _noviNaziv;
        set => SetField(ref _noviNaziv, value);
    }

    public RelayCommand DodajCommand { get; }

    public KategorijaListViewModel(KategorijaService kategorijaService)
    {
        _kategorijaService = kategorijaService;
        DodajCommand = new RelayCommand(_ => Dodaj(), _ => !string.IsNullOrWhiteSpace(NoviNaziv));
        Ucitaj();
    }

    private void Ucitaj()
    {
        Kategorije.Clear();
        foreach (var kategorija in _kategorijaService.PregledSvih())
            Kategorije.Add(kategorija);
    }

    private void Dodaj()
    {
        _kategorijaService.Kreiraj(NoviNaziv);
        NoviNaziv = string.Empty;
        Ucitaj();
    }
}
