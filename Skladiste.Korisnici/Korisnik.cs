namespace Skladiste.Korisnici;

// Entitet - domenski sloj modula Korisnici
public class Korisnik
{
    public int Id { get; set; }
    public string KorisnickoIme { get; set; } = string.Empty;
    public string LozinkaHash { get; set; } = string.Empty;
}
