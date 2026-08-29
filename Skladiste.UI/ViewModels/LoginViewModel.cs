using System;
using System.Windows.Controls;
using Skladiste.Korisnici;

namespace Skladiste.UI.ViewModels;

public class LoginViewModel : ViewModelBase
{
    private readonly AutentifikacijaService _autentifikacija;

    private string _korisnickoIme = string.Empty;
    public string KorisnickoIme
    {
        get => _korisnickoIme;
        set => SetField(ref _korisnickoIme, value);
    }

    private string _poruka = string.Empty;
    public string Poruka
    {
        get => _poruka;
        set => SetField(ref _poruka, value);
    }

    public bool Uspesno { get; private set; }

    public RelayCommand PrijaviSeCommand { get; }
    public event Action? ZatvoriProzor;

    public LoginViewModel(AutentifikacijaService autentifikacija)
    {
        _autentifikacija = autentifikacija;
        PrijaviSeCommand = new RelayCommand(parametar => PrijaviSe(parametar as PasswordBox));
    }

    // Use case: prijava korisnika
    private void PrijaviSe(PasswordBox? lozinkaPolje)
    {
        var korisnik = _autentifikacija.PrijaviSe(KorisnickoIme, lozinkaPolje?.Password ?? string.Empty);
        if (korisnik == null)
        {
            Poruka = "Pogresno korisnicko ime ili lozinka.";
            return;
        }

        SesijaServis.TrenutniKorisnik = korisnik;
        Uspesno = true;
        ZatvoriProzor?.Invoke();
    }
}
