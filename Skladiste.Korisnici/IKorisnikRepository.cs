using System.Collections.Generic;

namespace Skladiste.Korisnici;

// Port - repozitorijum sloj
public interface IKorisnikRepository
{
    List<Korisnik> GetAll();
    Korisnik? GetByKorisnickoIme(string korisnickoIme);
    void Add(Korisnik korisnik);
}
