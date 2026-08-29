using System.Collections.Generic;
using System.Linq;
using Skladiste.Proizvodi;

namespace Skladiste.Tests.Fakes;

// Fake adapter za testiranje bez UI/JSON zavisnosti
public class FakeProizvodRepository : IProizvodRepository
{
    private readonly List<Proizvod> _proizvodi = new();

    public List<Proizvod> GetAll() => _proizvodi;
    public Proizvod? GetById(int id) => _proizvodi.FirstOrDefault(p => p.Id == id);
    public void Add(Proizvod proizvod) => _proizvodi.Add(proizvod);
    public void Update(Proizvod proizvod) { }
    public void Delete(int id) => _proizvodi.RemoveAll(p => p.Id == id);
}
