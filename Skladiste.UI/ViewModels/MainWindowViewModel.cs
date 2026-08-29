using Skladiste.Kategorije;
using Skladiste.Nalozi;
using Skladiste.Proizvodi;

namespace Skladiste.UI.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public ProizvodListViewModel ProizvodListViewModel { get; }
    public KategorijaListViewModel KategorijaListViewModel { get; }
    public NalogListViewModel NalogListViewModel { get; }

    public MainWindowViewModel(ProizvodService proizvodService, KategorijaService kategorijaService, NalogService nalogService)
    {
        ProizvodListViewModel = new ProizvodListViewModel(proizvodService, kategorijaService);
        KategorijaListViewModel = new KategorijaListViewModel(kategorijaService);
        NalogListViewModel = new NalogListViewModel(nalogService, proizvodService);
    }
}
