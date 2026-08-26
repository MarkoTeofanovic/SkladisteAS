using System.Collections.Generic;
using System.Linq;

namespace Skladiste.Kategorije;

// Aplikacioni sloj - osnovne operacije (kreiranje, prikaz)
public class KategorijaService
{
    private readonly IKategorijaRepository _repozitorijum;

    public KategorijaService(IKategorijaRepository repozitorijum)
    {
        _repozitorijum = repozitorijum;
    }

    public List<Kategorija> PregledSvih() => _repozitorijum.GetAll();

    public Kategorija Kreiraj(string naziv)
    {
        var svi = _repozitorijum.GetAll();
        var kategorija = new Kategorija
        {
            Id = svi.Count == 0 ? 1 : svi.Max(k => k.Id) + 1,
            Naziv = naziv
        };
        _repozitorijum.Add(kategorija);
        return kategorija;
    }
}
