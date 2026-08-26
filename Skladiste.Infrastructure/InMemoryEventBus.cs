using System;
using System.Collections.Generic;
using Skladiste.SharedKernel;

namespace Skladiste.Infrastructure;

// Adapter: IEventBus port, drzi pretplatnike u memoriji
public class InMemoryEventBus : IEventBus
{
    private readonly Dictionary<Type, List<Delegate>> _pretplatnici = new();

    public void Publish<T>(T dogadjaj)
    {
        if (!_pretplatnici.TryGetValue(typeof(T), out var handleri))
            return;

        foreach (var handler in handleri)
            ((Action<T>)handler).Invoke(dogadjaj);
    }

    public void Subscribe<T>(Action<T> handler)
    {
        if (!_pretplatnici.TryGetValue(typeof(T), out var handleri))
        {
            handleri = new List<Delegate>();
            _pretplatnici[typeof(T)] = handleri;
        }

        handleri.Add(handler);
    }
}
