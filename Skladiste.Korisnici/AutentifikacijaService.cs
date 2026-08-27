using System;
using System.Security.Cryptography;
using System.Text;

namespace Skladiste.Korisnici;

// Use case: prijava korisnika
public class AutentifikacijaService
{
    private readonly IKorisnikRepository _repozitorijum;

    public AutentifikacijaService(IKorisnikRepository repozitorijum)
    {
        _repozitorijum = repozitorijum;
    }

    public static string Hesiraj(string lozinka)
    {
        var bajtovi = SHA256.HashData(Encoding.UTF8.GetBytes(lozinka));
        return Convert.ToHexString(bajtovi);
    }

    public Korisnik? PrijaviSe(string korisnickoIme, string lozinka)
    {
        var hash = Hesiraj(lozinka);
        var korisnik = _repozitorijum.GetByKorisnickoIme(korisnickoIme);
        return korisnik != null && korisnik.LozinkaHash == hash ? korisnik : null;
    }
}
