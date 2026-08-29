using Skladiste.Korisnici;

namespace Skladiste.UI;

// Drzi prijavljenog korisnika za trajanje sesije
public static class SesijaServis
{
    public static Korisnik? TrenutniKorisnik { get; set; }
}
