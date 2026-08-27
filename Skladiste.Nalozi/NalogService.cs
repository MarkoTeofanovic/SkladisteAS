using System;
using System.Collections.Generic;
using System.Linq;
using Skladiste.Proizvodi;
using Skladiste.SharedKernel;

namespace Skladiste.Nalozi;

// Slozenija operacija: kreiranje naloga sa stavkama - kalkulacija ukupne vrednosti i azuriranje stanja
public class NalogService
{
    private readonly INalogRepository _repozitorijum;
    private readonly ProizvodService _proizvodService;
    private readonly IEventBus _eventBus;

    public NalogService(INalogRepository repozitorijum, ProizvodService proizvodService, IEventBus eventBus)
    {
        _repozitorijum = repozitorijum;
        _proizvodService = proizvodService;
        _eventBus = eventBus;
    }

    public List<Nalog> PregledSvih() => _repozitorijum.GetAll();

    public Nalog KreirajNalog(string brojNaloga, int korisnikId, List<(int ProizvodId, int Kolicina)> stavke)
    {
        var proizvodi = _proizvodService.PregledSvih();
        var nalog = new Nalog
        {
            Id = NoviId(),
            BrojNaloga = brojNaloga,
            Datum = DateTime.Now,
            KorisnikId = korisnikId
        };

        foreach (var (proizvodId, kolicina) in stavke)
        {
            var proizvod = proizvodi.FirstOrDefault(p => p.Id == proizvodId);
            if (proizvod == null)
                continue;

            nalog.Stavke.Add(new StavkaNaloga
            {
                ProizvodId = proizvodId,
                Kolicina = kolicina,
                CenaPoKomadu = proizvod.Cena
            });

            nalog.UkupnaVrednost += proizvod.Cena * kolicina;

            // Poziva Proizvodi modul (jednosmerna zavisnost Nalozi -> Proizvodi) da azurira kolicinu na stanju
            _proizvodService.AzurirajKolicinu(proizvodId, kolicina);
        }

        _repozitorijum.Add(nalog);

        // Dogadjaj - Obavestenja modul reaguje na ovo
        _eventBus.Publish(new NalogKreiranEvent
        {
            NalogId = nalog.Id,
            BrojNaloga = nalog.BrojNaloga,
            UkupnaVrednost = nalog.UkupnaVrednost
        });

        return nalog;
    }

    private int NoviId()
    {
        var svi = _repozitorijum.GetAll();
        return svi.Count == 0 ? 1 : svi.Max(n => n.Id) + 1;
    }
}
