# Sistem za upravljanje skladistem

Desktop aplikacija (WPF, .NET 8) izgradjena kao modularni monolit, sa Clean i heksagonalnom arhitekturom. Podaci se cuvaju u JSON datotekama.

## Arhitektura

Aplikacija je podeljena u module:

- Skladiste.SharedKernel - zajednicki domenski elementi (dogadjaj NalogKreiranEvent, port IEventBus), bez zavisnosti od drugih modula
- Skladiste.Proizvodi - Proizvod, IProizvodRepository, ProizvodService
- Skladiste.Kategorije - Kategorija, IKategorijaRepository, KategorijaService
- Skladiste.Korisnici - Korisnik, IKorisnikRepository, AutentifikacijaService
- Skladiste.Nalozi - Nalog, StavkaNaloga, INalogRepository, NalogService (kreiranje naloga sa stavkama - kalkulacija i azuriranje stanja)
- Skladiste.Obavestenja - IObavestenjeSlanje, ObavestenjeHandler (reaguje na dogadjaj iz Nalozi modula)
- Skladiste.Infrastructure - adapteri za sve portove (JSON repozitorijumi, InMemoryEventBus, KonzolnoObavestenjeSlanje)
- Skladiste.UI - WPF kompozicioni koren (DI, ViewModeli, Views)
- Skladiste.Tests - jedinicni testovi nad aplikacionim slojem

Svaki funkcionalni modul sadrzi svoj entitet, port (interfejs) i aplikacioni servis. Konkretne implementacije portova (adapteri) nalaze se iskljucivo u Infrastructure modulu. Zavisnosti idu u jednom smeru: UI i Infrastructure zavise od funkcionalnih modula, Nalozi zavisi od Proizvodi, a Nalozi i Obavestenja zavise od SharedKernel. Nijedan modul ne zavisi kruzno od drugog.

Dependency Injection (Microsoft.Extensions.DependencyInjection) kabluje se na jednom mestu, u Skladiste.UI/App.xaml.cs.

Mehanizam dogadjaja: NalogService objavljuje NalogKreiranEvent preko IEventBus-a kada se kreira novi nalog. ObavestenjeHandler (Obavestenja modul) se pretplacuje na taj dogadjaj i belezi obavestenje, bez direktne zavisnosti od Nalozi modula.

## Pokretanje

    dotnet build
    dotnet run --project Skladiste.UI

Podrazumevani nalog za prijavu:
- Korisnicko ime: admin
- Lozinka: admin123

## Testiranje

    dotnet test

## Dokumentacija

U docs/ se nalaze UML/C4 dijagrami i projektna dokumentacija (Dokumentacija.pdf).
