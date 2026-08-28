using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Skladiste.SharedKernel;
using Skladiste.Infrastructure;
using Skladiste.Proizvodi;
using Skladiste.Kategorije;
using Skladiste.Korisnici;
using Skladiste.Nalozi;
using Skladiste.Obavestenja;
using Skladiste.UI.ViewModels;
using Skladiste.UI.Views;

namespace Skladiste.UI;

// Kompozicioni koren - ovde se kabluje ceo modularni monolit
public partial class App : Application
{
    private ServiceProvider? _servisi;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Sprecava gasenje aplikacije kad se zatvori prozor za prijavu
        ShutdownMode = ShutdownMode.OnExplicitShutdown;

        var kolekcija = new ServiceCollection();
        Registruj(kolekcija);
        _servisi = kolekcija.BuildServiceProvider();

        // Force-razresenje da ObavestenjeHandler odmah pretplati na dogadjaje
        _servisi.GetRequiredService<ObavestenjeHandler>();

        // Use case: prijava korisnika
        var prijava = _servisi.GetRequiredService<LoginWindow>();
        if (prijava.ShowDialog() != true)
        {
            Shutdown();
            return;
        }

        var glavniProzor = _servisi.GetRequiredService<MainWindow>();
        MainWindow = glavniProzor;
        ShutdownMode = ShutdownMode.OnMainWindowClose;
        glavniProzor.Show();
    }

    // DI - registracija portova, adaptera i aplikacionih servisa (Microsoft.Extensions.DependencyInjection)
    private static void Registruj(IServiceCollection servisi)
    {
        servisi.AddSingleton<IEventBus, InMemoryEventBus>();

        servisi.AddSingleton<IProizvodRepository, ProizvodJsonRepository>();
        servisi.AddSingleton<IKategorijaRepository, KategorijaJsonRepository>();
        servisi.AddSingleton<IKorisnikRepository, KorisnikJsonRepository>();
        servisi.AddSingleton<INalogRepository, NalogJsonRepository>();
        servisi.AddSingleton<IObavestenjeSlanje, KonzolnoObavestenjeSlanje>();

        servisi.AddSingleton<ProizvodService>();
        servisi.AddSingleton<KategorijaService>();
        servisi.AddSingleton<AutentifikacijaService>();
        servisi.AddSingleton<NalogService>();
        servisi.AddSingleton<ObavestenjeHandler>();

        servisi.AddTransient<LoginViewModel>();
        servisi.AddTransient<LoginWindow>();

        servisi.AddTransient<MainWindowViewModel>();
        servisi.AddTransient<MainWindow>();
    }
}
