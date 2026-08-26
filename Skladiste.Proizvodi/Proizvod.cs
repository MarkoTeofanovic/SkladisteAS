namespace Skladiste.Proizvodi;

// Entitet - domenski sloj modula Proizvodi
public class Proizvod
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public string Sifra { get; set; } = string.Empty;
    public decimal Cena { get; set; }
    public int KolicinaNaStanju { get; set; }
    public int KategorijaId { get; set; }
}
