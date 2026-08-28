using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Skladiste.UI.ViewModels;

public abstract class ViewModelBase : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected bool SetField<T>(ref T polje, T vrednost, [CallerMemberName] string? naziv = null)
    {
        if (EqualityComparer<T>.Default.Equals(polje, vrednost))
            return false;

        polje = vrednost;
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(naziv));
        return true;
    }
}
