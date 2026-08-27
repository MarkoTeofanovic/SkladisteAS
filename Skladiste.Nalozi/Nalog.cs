using System;
using System.Collections.Generic;

namespace Skladiste.Nalozi;

// Entitet - domenski sloj modula Nalozi. Rad sa vise povezanih entiteta (nalog - stavke - proizvod - korisnik).
public class Nalog
{
    public int Id { get; set; }
    public string BrojNaloga { get; set; } = string.Empty;
    public DateTime Datum { get; set; } = DateTime.Now;
    public int KorisnikId { get; set; }
    public List<StavkaNaloga> Stavke { get; set; } = new();
    public decimal UkupnaVrednost { get; set; }
}
