using Skladiste.Kategorije;
using Skladiste.Proizvodi;

namespace Skladiste.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ProizvodListViewModel ProizvodListViewModel { get; }
    public KategorijaListViewModel KategorijaListViewModel { get; }

    public MainWindowViewModel(ProizvodService proizvodService, KategorijaService kategorijaService)
    {
        ProizvodListViewModel = new ProizvodListViewModel(proizvodService, kategorijaService);
        KategorijaListViewModel = new KategorijaListViewModel(kategorijaService);
    }
}
