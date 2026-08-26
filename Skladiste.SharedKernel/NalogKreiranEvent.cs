namespace Skladiste.SharedKernel;

// Dogadjaj: objavljuje se kada Nalozi modul kreira novi nalog
public class NalogKreiranEvent
{
    public int NalogId { get; set; }
    public string BrojNaloga { get; set; } = string.Empty;
    public decimal UkupnaVrednost { get; set; }
}
