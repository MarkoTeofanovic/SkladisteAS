using System.Collections.Generic;
using System.Linq;

namespace Skladiste.Proizvodi;

// Aplikacioni sloj - osnovne operacije (kreiranje, izmena, prikaz)
public class ProizvodService
{
    private readonly IProizvodRepository _repozitorijum;

    public ProizvodService(IProizvodRepository repozitorijum)
    {
        _repozitorijum = repozitorijum;
    }

    public List<Proizvod> PregledSvih() => _repozitorijum.GetAll();

    public Proizvod Kreiraj(string naziv, string sifra, decimal cena, int kolicina, int kategorijaId)
    {
        var proizvod = new Proizvod
        {
            Id = NoviId(),
            Naziv = naziv,
            Sifra = sifra,
            Cena = cena,
            KolicinaNaStanju = kolicina,
            KategorijaId = kategorijaId
        };
        _repozitorijum.Add(proizvod);
        return proizvod;
    }

    public void Izmeni(int id, string naziv, string sifra, decimal cena, int kolicina, int kategorijaId)
    {
        var proizvod = _repozitorijum.GetById(id);
        if (proizvod == null)
            return;

        proizvod.Naziv = naziv;
        proizvod.Sifra = sifra;
        proizvod.Cena = cena;
        proizvod.KolicinaNaStanju = kolicina;
        proizvod.KategorijaId = kategorijaId;
        _repozitorijum.Update(proizvod);
    }

    public void Obrisi(int id) => _repozitorijum.Delete(id);

    // Koristi ga Nalozi modul za azuriranje stanja pri kreiranju naloga
    public void AzurirajKolicinu(int proizvodId, int promena)
    {
        var proizvod = _repozitorijum.GetById(proizvodId);
        if (proizvod == null)
            return;

        proizvod.KolicinaNaStanju += promena;
        _repozitorijum.Update(proizvod);
    }

    private int NoviId()
    {
        var svi = _repozitorijum.GetAll();
        return svi.Count == 0 ? 1 : svi.Max(p => p.Id) + 1;
    }
}
