using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Skladiste.Korisnici;

namespace Skladiste.Infrastructure;

// Adapter - IKorisnikRepository port, perzistencija u JSON datoteci
public class KorisnikJsonRepository : IKorisnikRepository
{
    private readonly string _putanja;

    public KorisnikJsonRepository(string putanja = "korisnici.json")
    {
        _putanja = putanja;
        if (!File.Exists(_putanja))
            Seeduj();
    }

    public List<Korisnik> GetAll()
    {
        var json = File.ReadAllText(_putanja);
        return JsonSerializer.Deserialize<List<Korisnik>>(json) ?? new List<Korisnik>();
    }

    public Korisnik? GetByKorisnickoIme(string korisnickoIme) =>
        GetAll().FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);

    public void Add(Korisnik korisnik)
    {
        var svi = GetAll();
        svi.Add(korisnik);
        Sacuvaj(svi);
    }

    // Podrazumevani nalog za prijavu: admin / admin123
    private void Seeduj()
    {
        var admin = new Korisnik
        {
            Id = 1,
            KorisnickoIme = "admin",
            LozinkaHash = "240BE518FABD2724DDB6F04EEB1DA5967448D7E831C08C8FA822809F74C720A9"
        };
        Sacuvaj(new List<Korisnik> { admin });
    }

    private void Sacuvaj(List<Korisnik> svi)
    {
        var json = JsonSerializer.Serialize(svi, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_putanja, json);
    }
}
