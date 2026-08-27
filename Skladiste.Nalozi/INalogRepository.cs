using System.Collections.Generic;

namespace Skladiste.Nalozi;

// Port - repozitorijum sloj
public interface INalogRepository
{
    List<Nalog> GetAll();
    void Add(Nalog nalog);
}
