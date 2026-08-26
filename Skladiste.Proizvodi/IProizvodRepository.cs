using System.Collections.Generic;

namespace Skladiste.Proizvodi;

// Port - repozitorijum sloj, razdvaja pristup podacima od logike
public interface IProizvodRepository
{
    List<Proizvod> GetAll();
    Proizvod? GetById(int id);
    void Add(Proizvod proizvod);
    void Update(Proizvod proizvod);
    void Delete(int id);
}
