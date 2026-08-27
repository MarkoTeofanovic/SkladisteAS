using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using Skladiste.Proizvodi;

namespace Skladiste.Infrastructure;

// Adapter - IProizvodRepository port, perzistencija u JSON datoteci
public class ProizvodJsonRepository : IProizvodRepository
{
    private readonly string _putanja;

    public ProizvodJsonRepository(string putanja = "proizvodi.json")
    {
        _putanja = putanja;
    }

    public List<Proizvod> GetAll()
    {
        if (!File.Exists(_putanja))
            return new List<Proizvod>();

        var json = File.ReadAllText(_putanja);
        return JsonSerializer.Deserialize<List<Proizvod>>(json) ?? new List<Proizvod>();
    }

    public Proizvod? GetById(int id) => GetAll().FirstOrDefault(p => p.Id == id);

    public void Add(Proizvod proizvod)
    {
        var svi = GetAll();
        svi.Add(proizvod);
        Sacuvaj(svi);
    }

    public void Update(Proizvod proizvod)
    {
        var svi = GetAll();
        var postojeci = svi.FirstOrDefault(p => p.Id == proizvod.Id);
        if (postojeci == null)
            return;

        svi.Remove(postojeci);
        svi.Add(proizvod);
        Sacuvaj(svi);
    }

    public void Delete(int id)
    {
        var svi = GetAll();
        svi.RemoveAll(p => p.Id == id);
        Sacuvaj(svi);
    }

    private void Sacuvaj(List<Proizvod> svi)
    {
        var json = JsonSerializer.Serialize(svi, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(_putanja, json);
    }
}
