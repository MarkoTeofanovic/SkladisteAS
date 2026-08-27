using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Skladiste.Kategorije;

namespace Skladiste.Infrastructure;

// Adapter - IKategorijaRepository port, perzistencija u JSON datoteci
public class KategorijaJsonRepository : IKategorijaRepository
{
    private readonly string _putanja;

    public KategorijaJsonRepository(string putanja = "kategorije.json")
    {
        _putanja = putanja;
    }

    public List<Kategorija> GetAll()
    {
        if (!File.Exists(_putanja))
            return new List<Kategorija>();

        var json = File.ReadAllText(_putanja);
        return JsonSerializer.Deserialize<List<Kategorija>>(json) ?? new List<Kategorija>();
    }

    public Kategorija? GetById(int id) => GetAll().FirstOrDefault(k => k.Id == id);

    public void Add(Kategorija kategorija)
    {
        var sve = GetAll();
        sve.Add(kategorija);
        var json = JsonSerializer.Serialize(sve, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_putanja, json);
    }
}
