using System.Collections.Generic;

namespace Skladiste.Kategorije;

// Port - repozitorijum sloj
public interface IKategorijaRepository
{
    List<Kategorija> GetAll();
    Kategorija? GetById(int id);
    void Add(Kategorija kategorija);
}
