namespace Skladiste.Obavestenja;

// Port - spoljna zavisnost (slanje obavestenja)
public interface IObavestenjeSlanje
{
    void Posalji(string poruka);
}
