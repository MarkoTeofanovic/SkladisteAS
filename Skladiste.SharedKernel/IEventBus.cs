using System;

namespace Skladiste.SharedKernel;

// Port: mehanizam dogadjaja - moduli objavljuju i pretplacuju se preko ovog interfejsa
public interface IEventBus
{
    void Publish<T>(T dogadjaj);
    void Subscribe<T>(Action<T> handler);
}
