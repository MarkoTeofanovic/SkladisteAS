using System;
using System.Windows.Input;

namespace Skladiste.UI.ViewModels;

public class RelayCommand : ICommand
{
    private readonly Action<object?> _izvrsi;
    private readonly Func<object?, bool>? _mozeIzvrsiti;

    public RelayCommand(Action<object?> izvrsi, Func<object?, bool>? mozeIzvrsiti = null)
    {
        _izvrsi = izvrsi;
        _mozeIzvrsiti = mozeIzvrsiti;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parametar) => _mozeIzvrsiti?.Invoke(parametar) ?? true;

    public void Execute(object? parametar) => _izvrsi(parametar);
}
