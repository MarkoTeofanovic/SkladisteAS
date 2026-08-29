using System;
using System.Collections.ObjectModel;
using System.Linq;
using Skladiste.Kategorije;
using Skladiste.Proizvodi;

namespace Skladiste.UI.ViewModels;

public class ProizvodDetaljiViewModel : ViewModelBase
{
    private readonly ProizvodService _proizvodService;
    private readonly int? _id;

    public ObservableCollection<Kategorija> Kategorije { get; } = new();

    private string _naziv = string.Empty;
    public string Naziv
    {
        get => _naziv;
        set => SetField(ref _naziv, value);
    }

    private string _sifra = string.Empty;
    public string Sifra
    {
        get => _sifra;
        set => SetField(ref _sifra, value);
    }

    private decimal _cena;
    public decimal Cena
    {
        get => _cena;
        set => SetField(ref _cena, value);
    }

    private int _kolicinaNaStanju;
    public int KolicinaNaStanju
    {
        get => _kolicinaNaStanju;
        set => SetField(ref _kolicinaNaStanju, value);
    }

    private Kategorija? _izabranaKategorija;
    public Kategorija? IzabranaKategorija
    {
        get => _izabranaKategorija;
        set => SetField(ref _izabranaKategorija, value);
    }

    public bool Sacuvano { get; private set; }

    public RelayCommand SacuvajCommand { get; }
    public RelayCommand OtkaziCommand { get; }
    public event Action? ZatvoriProzor;

    public ProizvodDetaljiViewModel(ProizvodService proizvodService, KategorijaService kategorijaService, Proizvod? postojeci)
    {
        _proizvodService = proizvodService;

        foreach (var kategorija in kategorijaService.PregledSvih())
            Kategorije.Add(kategorija);

        if (postojeci != null)
        {
            _id = postojeci.Id;
            Naziv = postojeci.Naziv;
            Sifra = postojeci.Sifra;
            Cena = postojeci.Cena;
            KolicinaNaStanju = postojeci.KolicinaNaStanju;
            IzabranaKategorija = Kategorije.FirstOrDefault(k => k.Id == postojeci.KategorijaId);
        }

        SacuvajCommand = new RelayCommand(_ => Sacuvaj(), _ => MozeSacuvati());
        OtkaziCommand = new RelayCommand(_ => ZatvoriProzor?.Invoke());
    }

    private bool MozeSacuvati()
        => !string.IsNullOrWhiteSpace(Naziv) && !string.IsNullOrWhiteSpace(Sifra) && IzabranaKategorija != null;

    // Use case: kreiranje / izmena proizvoda
    private void Sacuvaj()
    {
        if (_id.HasValue)
            _proizvodService.Izmeni(_id.Value, Naziv, Sifra, Cena, KolicinaNaStanju, IzabranaKategorija!.Id);
        else
            _proizvodService.Kreiraj(Naziv, Sifra, Cena, KolicinaNaStanju, IzabranaKategorija!.Id);

        Sacuvano = true;
        ZatvoriProzor?.Invoke();
    }
}
