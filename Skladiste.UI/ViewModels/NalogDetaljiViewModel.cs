using System;
using System.Collections.ObjectModel;
using System.Linq;
using Skladiste.Nalozi;
using Skladiste.Proizvodi;

namespace Skladiste.UI.ViewModels;

public class NalogDetaljiViewModel : ViewModelBase
{
    private readonly NalogService _nalogService;

    public ObservableCollection<Proizvod> Proizvodi { get; } = new();
    public ObservableCollection<StavkaUnosa> Stavke { get; } = new();

    private string _brojNaloga = string.Empty;
    public string BrojNaloga
    {
        get => _brojNaloga;
        set => SetField(ref _brojNaloga, value);
    }

    private Proizvod? _izabranProizvod;
    public Proizvod? IzabranProizvod
    {
        get => _izabranProizvod;
        set => SetField(ref _izabranProizvod, value);
    }

    private int _kolicinaZaDodavanje = 1;
    public int KolicinaZaDodavanje
    {
        get => _kolicinaZaDodavanje;
        set => SetField(ref _kolicinaZaDodavanje, value);
    }

    public bool Sacuvano { get; private set; }

    public RelayCommand DodajStavkuCommand { get; }
    public RelayCommand SacuvajCommand { get; }
    public RelayCommand OtkaziCommand { get; }
    public event Action? ZatvoriProzor;

    public NalogDetaljiViewModel(NalogService nalogService, ProizvodService proizvodService)
    {
        _nalogService = nalogService;

        foreach (var proizvod in proizvodService.PregledSvih())
            Proizvodi.Add(proizvod);

        DodajStavkuCommand = new RelayCommand(_ => DodajStavku(), _ => IzabranProizvod != null && KolicinaZaDodavanje > 0);
        SacuvajCommand = new RelayCommand(_ => Sacuvaj(), _ => MozeSacuvati());
        OtkaziCommand = new RelayCommand(_ => ZatvoriProzor?.Invoke());
    }

    private void DodajStavku()
    {
        Stavke.Add(new StavkaUnosa { Proizvod = IzabranProizvod!, Kolicina = KolicinaZaDodavanje });
        KolicinaZaDodavanje = 1;
    }

    private bool MozeSacuvati() => !string.IsNullOrWhiteSpace(BrojNaloga) && Stavke.Count > 0;

    // Slozenija operacija: kreiranje naloga sa stavkama
    private void Sacuvaj()
    {
        var stavke = Stavke.Select(s => (s.Proizvod.Id, s.Kolicina)).ToList();
        var korisnikId = SesijaServis.TrenutniKorisnik?.Id ?? 0;
        _nalogService.KreirajNalog(BrojNaloga, korisnikId, stavke);

        Sacuvano = true;
        ZatvoriProzor?.Invoke();
    }
}
