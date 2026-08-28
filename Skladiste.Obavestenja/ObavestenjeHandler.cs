using Skladiste.SharedKernel;

namespace Skladiste.Obavestenja;

// Reakcija drugog modula na dogadjaj - pretplacuje se na NalogKreiranEvent iz Nalozi modula
public class ObavestenjeHandler
{
    public ObavestenjeHandler(IEventBus eventBus, IObavestenjeSlanje slanje)
    {
        eventBus.Subscribe<NalogKreiranEvent>(dogadjaj =>
        {
            slanje.Posalji($"Nalog {dogadjaj.BrojNaloga} kreiran. Ukupna vrednost: {dogadjaj.UkupnaVrednost}");
        });
    }
}
