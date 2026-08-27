using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Skladiste.Nalozi;

namespace Skladiste.Infrastructure;

// Adapter - INalogRepository port, perzistencija u JSON datoteci
public class NalogJsonRepository : INalogRepository
{
    private readonly string _putanja;

    public NalogJsonRepository(string putanja = "nalozi.json")
    {
        _putanja = putanja;
    }

    public List<Nalog> GetAll()
    {
        if (!File.Exists(_putanja))
            return new List<Nalog>();

        var json = File.ReadAllText(_putanja);
        return JsonSerializer.Deserialize<List<Nalog>>(json) ?? new List<Nalog>();
    }

    public void Add(Nalog nalog)
    {
        var svi = GetAll();
        svi.Add(nalog);
        var json = JsonSerializer.Serialize(svi, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_putanja, json);
    }
}
