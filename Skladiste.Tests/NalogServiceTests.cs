using System.Collections.Generic;
using System.Linq;
using Skladiste.Nalozi;
using Skladiste.Proizvodi;
using Skladiste.SharedKernel;
using Skladiste.Tests.Fakes;
using Xunit;

namespace Skladiste.Tests;

// Testiranje slozenije operacije (Application sloj) bez UI zavisnosti
public class NalogServiceTests
{
    [Fact]
    public void KreirajNalog_IzracunavaUkupnuVrednostIAzurirajeStanje()
    {
        var proizvodService = new ProizvodService(new FakeProizvodRepository());
        var proizvod = proizvodService.Kreiraj("Busilica", "AL-001", 100, 10, 1);

        var nalogService = new NalogService(new FakeNalogRepository(), proizvodService, new FakeEventBus());

        var nalog = nalogService.KreirajNalog("PR-1", 1, new List<(int, int)> { (proizvod.Id, 3) });

        Assert.Equal(300, nalog.UkupnaVrednost);
        Assert.Equal(13, proizvodService.PregledSvih().First().KolicinaNaStanju);
    }

    [Fact]
    public void KreirajNalog_ObjavljujeNalogKreiranEvent()
    {
        var proizvodService = new ProizvodService(new FakeProizvodRepository());
        var proizvod = proizvodService.Kreiraj("Toner", "KM-001", 50, 20, 1);

        var eventBus = new FakeEventBus();
        var nalogService = new NalogService(new FakeNalogRepository(), proizvodService, eventBus);

        nalogService.KreirajNalog("PR-2", 1, new List<(int, int)> { (proizvod.Id, 2) });

        Assert.Single(eventBus.ObjavljeniDogadjaji);
        Assert.IsType<NalogKreiranEvent>(eventBus.ObjavljeniDogadjaji[0]);
    }
}
