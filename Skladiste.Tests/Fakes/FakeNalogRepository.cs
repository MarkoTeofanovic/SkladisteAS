using System.Collections.Generic;
using Skladiste.Nalozi;

namespace Skladiste.Tests.Fakes;

public class FakeNalogRepository : INalogRepository
{
    private readonly List<Nalog> _nalozi = new();

    public List<Nalog> GetAll() => _nalozi;
    public void Add(Nalog nalog) => _nalozi.Add(nalog);
}
