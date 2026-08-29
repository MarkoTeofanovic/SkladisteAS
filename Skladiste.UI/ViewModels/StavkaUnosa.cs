using Skladiste.Proizvodi;

namespace Skladiste.UI.ViewModels;

// Pomocna klasa za unos stavke naloga u dijalogu (nije domenski entitet)
public class StavkaUnosa
{
    public Proizvod Proizvod { get; set; } = null!;
    public int Kolicina { get; set; }
}
