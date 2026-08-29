using System.Windows;
using Skladiste.Obavestenja;

namespace Skladiste.Infrastructure;

// Adapter - IObavestenjeSlanje port, prikazuje obavestenje korisniku na ekranu
public class PopupObavestenjeSlanje : IObavestenjeSlanje
{
    public void Posalji(string poruka)
    {
        MessageBox.Show(poruka, "Obavestenje", MessageBoxButton.OK, MessageBoxImage.Information);
    }
}
