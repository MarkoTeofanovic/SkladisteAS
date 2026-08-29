using System.Linq;
using Skladiste.Proizvodi;
using Skladiste.Tests.Fakes;
using Xunit;

namespace Skladiste.Tests;

// Testiranje Application sloja bez UI zavisnosti
public class ProizvodServiceTests
{
    [Fact]
    public void Kreiraj_DodajeProizvodURepozitorijum()
    {
        var servis = new ProizvodService(new FakeProizvodRepository());

        servis.Kreiraj("Busilica", "AL-001", 6500, 10, 1);

        Assert.Single(servis.PregledSvih());
    }

    [Fact]
    public void Izmeni_MenjaPodatkePostojecegProizvoda()
    {
        var servis = new ProizvodService(new FakeProizvodRepository());
        var proizvod = servis.Kreiraj("Busilica", "AL-001", 6500, 10, 1);

        servis.Izmeni(proizvod.Id, "Busilica Bosch", "AL-002", 7000, 15, 1);

        var azuriran = servis.PregledSvih().First();
        Assert.Equal("Busilica Bosch", azuriran.Naziv);
        Assert.Equal(15, azuriran.KolicinaNaStanju);
    }

    [Fact]
    public void AzurirajKolicinu_PovecavaStanje()
    {
        var servis = new ProizvodService(new FakeProizvodRepository());
        var proizvod = servis.Kreiraj("Toner", "KM-001", 3200, 5, 2);

        servis.AzurirajKolicinu(proizvod.Id, 10);

        Assert.Equal(15, servis.PregledSvih().First().KolicinaNaStanju);
    }

    [Fact]
    public void Obrisi_UklanjaProizvod()
    {
        var servis = new ProizvodService(new FakeProizvodRepository());
        var proizvod = servis.Kreiraj("Toner", "KM-001", 3200, 5, 2);

        servis.Obrisi(proizvod.Id);

        Assert.Empty(servis.PregledSvih());
    }
}
